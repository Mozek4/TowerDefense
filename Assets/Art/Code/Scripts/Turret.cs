using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeBpsButton;
    [SerializeField] private Button upgradeRangeButton;
    [SerializeField] private Button sellTower;
    [SerializeField] private LineRenderer rangeIndicator;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f;
    [SerializeField] private int baseTargetingRangeUpgradeCost = 100;
    [SerializeField] private int baseBpsUpgradeCost = 100;

    private float targetingRangeBase = 5f;
    private float bpsBase = 1f;

    private Transform target;
    private float timeUntilFire;
    public GameObject bulletObj;
    private int bpsLevel = 1;
    private int rangeLevel = 1;

    private int towerSellCost;

    private void Start() {
        towerSellCost = LevelManager.main.towerCost;
        bpsBase = bps;
        targetingRangeBase = targetingRange;
        upgradeBpsButton.onClick.AddListener(UpgradeBps);
        upgradeRangeButton.onClick.AddListener(UpgradeRange);
        sellTower.onClick.AddListener(SellTower);

        rangeIndicator.positionCount = 0;
    }

    private void UpgradeButtonsManager() {
        if (bpsLevel == 6) {
            upgradeBpsButton.interactable = false;
        }
        if (rangeLevel == 6) {
            upgradeRangeButton.interactable = false;
        }
    }
    private void Update() {
        UpgradeButtonsManager();
        if (target == null) {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) {
            target = null;
        }
        else {

            timeUntilFire += Time.deltaTime;
        
            if (timeUntilFire >= 1f/bps) {
                Shoot();
                timeUntilFire = 0f;
            }  
        }
    }
    private void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;
        }

    }
    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
    private void RotateTowardsTarget() {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OpenUpgradeUI () {
        upgradeUI.SetActive(true);
        rangeIndicator.enabled = true;
        DrawRangeCircle();
    }

    public void CloseUpgradeUI () {
        upgradeUI.SetActive(false);
        rangeIndicator.enabled = false;
        UIManager.main.SetHoveringState(false);
    }

    private void DrawRangeCircle() {
        int segments = 50;
        float angleStep = 360f / segments;
        Vector3[] positions = new Vector3[segments + 1];

        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            float x = Mathf.Cos(angle) * targetingRange;
            float y = Mathf.Sin(angle) * targetingRange;
            positions[i] = new Vector3(x, y, 0);
        }

        rangeIndicator.positionCount = positions.Length;
        rangeIndicator.SetPositions(positions);
    }

    private void UpgradeBps() {
        if (CalculateBpsCost() > LevelManager.main.currency) {
            return;
        }
        LevelManager.main.SpendCurrency(CalculateBpsCost());
        bpsLevel ++;
        bps = CalculateBPS();
        CloseUpgradeUI();
    }

    private void UpgradeRange() {
        if (CalculateRangeCost() > LevelManager.main.currency) {
            return;
        }
        LevelManager.main.SpendCurrency(CalculateRangeCost());
        rangeLevel++;
        targetingRange = CalculateRange();
        CloseUpgradeUI();
    }

    public int CalculateBpsCost() {
        return Mathf.RoundToInt(baseBpsUpgradeCost * Mathf.Pow(bpsLevel, 1.1f));
    }

    public int CalculateRangeCost () {
        return Mathf.RoundToInt(baseTargetingRangeUpgradeCost * Mathf.Pow(rangeLevel, 1.1f));
    }
    
    private float CalculateBPS() {
        return bpsBase * Mathf.Pow(bpsLevel, 0.4f);
    }
    
    private float CalculateRange() {
        return targetingRangeBase * Mathf.Pow(rangeLevel, 0.15f);
    }

    private void SellTower () {
        Destroy(gameObject);
        LevelManager.main.currency += towerSellCost;
        CloseUpgradeUI();
    }
}
