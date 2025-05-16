using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
public class MageTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeBpsButton;
    [SerializeField] private Button upgradeRangeButton;
    [SerializeField] private Button sellTower;
    [SerializeField] private LineRenderer rangeIndicator;
    //[SerializeField] private AudioClip shot;

    [Header("Attribute")]
    [SerializeField] private float range = 5f;
    [SerializeField] private float aps = 1f;
    [SerializeField] private float baseRangeUpgradeCost = 100;
    [SerializeField] private int baseApsUpgradeCost = 100;

    private float rangeBase = 5f;
    private float apsbase = 1f;

    private Transform target;
    private float timeUntilFire;
    public GameObject bulletObj;
    private int bpsLevel = 1;
    private int rangeLevel = 1;

    private int towerSellCost;

    private void Start() {
        towerSellCost = LevelManager.main.towerCost;
        apsbase = aps;
        rangeBase = range;
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

        if (!CheckTargetIsInRange()) {
            target = null;
        }
        else {

            timeUntilFire += Time.deltaTime;
        
            if (timeUntilFire >= 1f/aps) {
                Shoot();
                timeUntilFire = 0f;
            }  
        }
    }
    private void Shoot() {
        //AudioSource.PlayClipAtPoint(shot, transform.position, 0.8f);
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;
        }

    }
    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= range;
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
            float x = Mathf.Cos(angle) * range;
            float y = Mathf.Sin(angle) * range;
            positions[i] = new Vector3(x, y, 0);
        }

        rangeIndicator.positionCount = positions.Length;
        rangeIndicator.SetPositions(positions);
    }

    private void UpgradeBps() {
        if (CalculateBpsCost() > LevelManager.main.gold) {
            return;
        }
        LevelManager.main.SpendCurrency(CalculateBpsCost());
        bpsLevel ++;
        aps = CalculateBPS();
        CloseUpgradeUI();
    }

    private void UpgradeRange() {
        if (CalculateRangeCost() > LevelManager.main.gold) {
            return;
        }
        LevelManager.main.SpendCurrency(CalculateRangeCost());
        rangeLevel++;
        range = CalculateRange();
        CloseUpgradeUI();
    }

    public int CalculateBpsCost() {
        return Mathf.RoundToInt(baseApsUpgradeCost * Mathf.Pow(bpsLevel, 1.1f));
    }

    public int CalculateRangeCost () {
        return Mathf.RoundToInt(baseRangeUpgradeCost * Mathf.Pow(rangeLevel, 1.1f));
    }

    private float CalculateBPS() {
        return apsbase * Mathf.Pow(bpsLevel, 0.4f);
    }
    
    private float CalculateRange() {
        return rangeBase * Mathf.Pow(rangeLevel, 0.15f);
    }

    private void SellTower () {
        Destroy(gameObject);
        LevelManager.main.gold += towerSellCost;
        CloseUpgradeUI();
    }

/*     private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    } */
}
