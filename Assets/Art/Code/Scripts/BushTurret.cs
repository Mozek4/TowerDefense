using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BushTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button sellTower;
    [SerializeField] private LineRenderer rangeIndicator;

    [Header("Attribute")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 1f;

    private int towerSellCost;

    private float timeUntilFire;

    private void Start()
    {
        towerSellCost = LevelManager.main.towerCost;   
        sellTower.onClick.AddListener(SellTower);

        rangeIndicator.positionCount = 0;
    }
    private void Update() {
        timeUntilFire += Time.deltaTime;
        
        if (timeUntilFire >= 1f/aps) {
            DamageEnemies();
            timeUntilFire = 0f;
        }  
    }

    private void DamageEnemies() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        for(int i = 0; i < hits.Length; i++) {
            Health Health = hits[i].transform.GetComponent<Health>();
            if (Health != null) {
                Health.TakeDamage((int)damage);
            }
        }
    }

    private void DrawRangeCircle(){
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

    private void SellTower() {
        Destroy(gameObject);
        LevelManager.main.currency += towerSellCost;
        CloseUpgradeUI();
    }
    
    public void OpenUpgradeUI() {
        upgradeUI.SetActive(true);
        rangeIndicator.enabled = true;
        DrawRangeCircle();
    }

    public void CloseUpgradeUI() {
        upgradeUI.SetActive(false);
        rangeIndicator.enabled = false;
        UIManager.main.SetHoveringState(false);
    }
}
