using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private bool shouldRotate = false;
    [SerializeField] private Transform turretRotationPoint; // Používá se pro plynulé otáčení (pokud shouldRotate = true)
    
    // --- ZMĚNA: Reference na objekt, který se má překlápět (flipovat) ---
    [Tooltip("Přiřaď sem grafiku/sprite, který se má otáčet vlevo/vpravo, pokud je 'shouldRotate' vypnuté.")]
    [SerializeField] private Transform spriteFlipperPoint; 
    // --------------------------------------------------------------------

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
    [SerializeField] private DamageType damageType = DamageType.Physical; 

    [SerializeField] private int baseDamage = 10;          
    [SerializeField] private GameObject meleeHitEffect;    


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

    protected virtual void Start()
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

        float effectiveRange = CalculateRange();
        float effectiveAps = CalculateBPS();

        if (target == null)
        {
            FindTarget(effectiveRange);
            return;
        }

        // --- ZMĚNA: Rozhodování o způsobu otáčení ---
        if (shouldRotate && turretRotationPoint != null)
        {
            // Původní plynulé otáčení
            RotateTowardsTarget();
        }
        else if (!shouldRotate && spriteFlipperPoint != null)
        {
            // Nové skokové otáčení (Left/Right)
            RotateLeftOrRight();
        }
        // --------------------------------------------

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

    // --- ZMĚNA: Nová funkce pro otáčení vlevo/vpravo ---
    private void RotateLeftOrRight()
    {
        if (target == null) return;

        // Pokud je target napravo od věže (X je větší)
        if (target.position.x > transform.position.x)
        {
            // Otoč se doprava (reset rotace na 0)
            spriteFlipperPoint.localRotation = Quaternion.Euler(0, 0, 0);
        }
        // Pokud je target nalevo od věže (X je menší)
        else
        {
            // Otoč se doleva (otočení o 180 stupňů kolem osy Y)
            spriteFlipperPoint.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    // ---------------------------------------------------

    protected virtual void Shoot()
    {
        if (shotSound != null)
            AudioSource.PlayClipAtPoint(shotSound, transform.position, 1.2f);

        int finalDamage = CalculateOutputDamage();

        if (attackType == AttackType.Ranged)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            Bullet bulletScript = bulletObj.GetComponent<Bullet>();

            bulletScript.SetTarget(target);
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

        if (meleeHitEffect != null)
            Instantiate(meleeHitEffect, target.position, Quaternion.identity);

        Health health = target.GetComponent<Health>();
        if (health != null)
            health.TakeDamage(damage, damageType); 
    }

    protected virtual int CalculateOutputDamage()
    {
        float damage = baseDamage;
        damage *= Mathf.Pow(bpsLevel, 0.5f);

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
        float effectiveRange = CalculateRange();

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
        float localBps = apsBase * Mathf.Pow(bpsLevel, 0.4f);
        float globalApsMult = PlayerStats.instance != null ? PlayerStats.instance.towerAttackSpeedMultiplier : 1f;
        return localBps * globalApsMult;
    }

    public float CalculateRange()
    {
        float localRange = rangeBase * Mathf.Pow(rangeLevel, 0.15f);
        float globalRangeMult = PlayerStats.instance != null ? PlayerStats.instance.towerRangeMultiplier : 1f;
        return localRange * globalRangeMult;
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
        Gizmos.color = Color.green;

        float effectiveRange;

        if (Application.isPlaying)
        {
            effectiveRange = CalculateRange();
        }
        else
        {
            effectiveRange = range * Mathf.Pow(rangeLevel, 0.15f);
        }

        Gizmos.DrawWireSphere(transform.position, effectiveRange);
    }
}