using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private bool shouldRotate = false;
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] public LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeBpsButton;
    [SerializeField] private Button upgradeRangeButton;
    [SerializeField] private Button sellTower;
    [SerializeField] private LineRenderer rangeIndicator;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Attributes")]
    [SerializeField] private float range = 5f;
    [SerializeField] private float aps = 1f;
    [SerializeField] private int baseRangeUpgradeCost = 100;
    [SerializeField] private int baseApsUpgradeCost = 100;

    [Header("Upgrade Bars")]
    [SerializeField] private List<GameObject> bpsBars;    // ← ČÁRKY PRO BPS
    [SerializeField] private List<GameObject> rangeBars;  // ← ČÁRKY PRO RANGE

    public enum AttackType
    {
        Ranged,
        Melee
    }

    [Header("Attack Settings")]
    [SerializeField] private AttackType attackType = AttackType.Ranged;
    [SerializeField] private DamageType damageType = DamageType.Physical; // Nové: Věž ví, jaký typ poškození dává (nastav v Inspectoru)

    [SerializeField] private int baseDamage = 10;          // základní damage věže
    [SerializeField] private GameObject meleeHitEffect;    // efekt zásahu (optional)


    private float rangeBase;
    private float apsBase;

    protected Transform target; 
    private float timeUntilFire;
    private int bpsLevel = 1;
    private int rangeLevel = 1;
    private int towerSellCost;



    public int BpsLevel => bpsLevel;
    public float ApsBaseTimes(float x) => apsBase * x;

    public int RangeLevel => rangeLevel;
    public float RangeBaseTimes(float x) => rangeBase * x;

    private void Start()
    {
        towerSellCost = LevelManager.main.towerCost;
        apsBase = aps;
        rangeBase = range;

        upgradeBpsButton.onClick.AddListener(UpgradeBps);
        upgradeRangeButton.onClick.AddListener(UpgradeRange);
        sellTower.onClick.AddListener(SellTower);

        rangeIndicator.positionCount = 0;
    }

    private void Update()
    {
        UpgradeButtonsManager();

        float globalRangeMult = PlayerStats.instance != null ? PlayerStats.instance.towerRangeMultiplier : 1f;
        float globalApsMult = PlayerStats.instance != null ? PlayerStats.instance.towerAttackSpeedMultiplier : 1f;

        float effectiveRange = CalculateRange() * globalRangeMult;
        float effectiveAps = CalculateBPS() * globalApsMult;

        if (target == null)
        {
            FindTarget(effectiveRange);
            return;
        }

        if (shouldRotate && turretRotationPoint != null)
            RotateTowardsTarget();

        if (!CheckTargetIsInRange(effectiveRange))
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / effectiveAps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void RotateTowardsTarget()
    {
        if (target == null) return;
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    protected virtual void Shoot()
    {
        if (shotSound != null)
            AudioSource.PlayClipAtPoint(shotSound, transform.position, 1.2f);

        // Vypočítáme damage centrálně zde
        int finalDamage = CalculateOutputDamage();

        if (attackType == AttackType.Ranged)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            Bullet bulletScript = bulletObj.GetComponent<Bullet>();

            bulletScript.SetTarget(target);
            // Předáme damage a typ kulce
            bulletScript.SetupBullet(finalDamage, damageType);
        }
        else if (attackType == AttackType.Melee)
        {
            DoMeleeAttack(finalDamage);
        }
    }


    protected virtual void DoMeleeAttack(int damage)
    {
        if (target == null) return;

        // efekt zásahu
        if (meleeHitEffect != null)
            Instantiate(meleeHitEffect, target.position, Quaternion.identity);

        // damage
        Health health = target.GetComponent<Health>();
        if (health != null)
            health.TakeDamage(damage, damageType); // Předáváme i typ poškození
    }

    // Centrální výpočet síly věže
    protected virtual int CalculateOutputDamage()
    {
        float damage = baseDamage;

        // upgrade věže (levely)
        damage *= Mathf.Pow(bpsLevel, 0.5f);

        // globální buffy (PlayerStats) - ZDE SE APLIKUJE MULTIPLIER (pouze jednou!)
        if (PlayerStats.instance != null)
            damage *= PlayerStats.instance.towerDamageMultiplier;

        return Mathf.RoundToInt(damage);
    }

    private void FindTarget(float effectiveRange)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectiveRange, Vector2.zero, 0f, enemyMask);
        if (hits.Length > 0)
            target = hits[0].transform;
    }

    private bool CheckTargetIsInRange(float effectiveRange)
    {
        if (target == null) return false;
        return Vector2.Distance(target.position, transform.position) <= effectiveRange;
    }

    private void UpgradeButtonsManager()
    {
        upgradeBpsButton.interactable = bpsLevel < 6;
        upgradeRangeButton.interactable = rangeLevel < 6;
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
        rangeIndicator.enabled = true;
        DrawRangeCircle();
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        rangeIndicator.enabled = false;
        UIManager.main.SetHoveringState(false);
    }

    private void DrawRangeCircle()
    {
        float globalRangeMult = PlayerStats.instance != null ? PlayerStats.instance.towerRangeMultiplier : 1f;
        float effectiveRange = CalculateRange() * globalRangeMult;

        int segments = 50;
        float angleStep = 360f / segments;
        Vector3[] positions = new Vector3[segments + 1];

        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            float x = Mathf.Cos(angle) * effectiveRange;
            float y = Mathf.Sin(angle) * effectiveRange;
            positions[i] = new Vector3(x, y, 0);
        }

        rangeIndicator.positionCount = positions.Length;
        rangeIndicator.SetPositions(positions);
    }

    private void UpgradeBps()
    {
        if (CalculateBpsCost() > LevelManager.main.gold) return;
        LevelManager.main.SpendCurrency(CalculateBpsCost());
        bpsLevel++;
        CloseUpgradeUI();
        UpdateBpsBars();
    }

    private void UpgradeRange()
    {
        if (CalculateRangeCost() > LevelManager.main.gold) return;
        LevelManager.main.SpendCurrency(CalculateRangeCost());
        rangeLevel++;
        CloseUpgradeUI();
        UpdateRangeBars();
    }

    public int CalculateBpsCost()
    {
        return Mathf.RoundToInt(baseApsUpgradeCost * Mathf.Pow(bpsLevel, 1.1f));
    }

    public int CalculateRangeCost()
    {
        return Mathf.RoundToInt(baseRangeUpgradeCost * Mathf.Pow(rangeLevel, 1.1f));
    }

    public float CalculateBPS()
    {
        return apsBase * Mathf.Pow(bpsLevel, 0.4f);
    }

    public float CalculateRange()
    {
        return rangeBase * Mathf.Pow(rangeLevel, 0.15f);
    }

    private void SellTower()
    {
        Destroy(gameObject);
        LevelManager.main.gold += towerSellCost;
        CloseUpgradeUI();
    }

    private void UpdateBpsBars()
    {
        for (int i = 0; i < bpsBars.Count; i++)
            bpsBars[i].SetActive(i < bpsLevel);
    }

    private void UpdateRangeBars()
    {
        for (int i = 0; i < rangeBars.Count; i++)
            rangeBars[i].SetActive(i < rangeLevel);
    }

    private void OnDrawGizmosSelected()
    {
        // Barva gizma
        Gizmos.color = Color.green;

        // Pokud ještě Start neběžel, použij serializované hodnoty z Inspectoru
        float effectiveRange = range;
        int currentRangeLevel = rangeLevel;

        if (Application.isPlaying)
        {
            // Ve hře použij upravené hodnoty
            float globalRangeMult = PlayerStats.instance != null ? PlayerStats.instance.towerRangeMultiplier : 1f;
            effectiveRange = CalculateRange() * globalRangeMult;
        }
        else
        {
            // Editor: použij základní hodnoty a level z Inspectoru
            effectiveRange = range * Mathf.Pow(currentRangeLevel, 0.15f);
        }

        // Vykresli kruh
        Gizmos.DrawWireSphere(transform.position, effectiveRange);
    }


}