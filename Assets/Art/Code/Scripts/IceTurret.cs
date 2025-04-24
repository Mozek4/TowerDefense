using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class IceTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button sellTower;
    [SerializeField] private LineRenderer rangeIndicator;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 1f;
    [SerializeField] private float freezeTime = 1f;

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
            FreezeEnemies();
            timeUntilFire = 0f;
        }  
    }

    private void FreezeEnemies() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0) {
            for(int i = 0; i < hits.Length; i++) {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                if (em != null) {
                    em.UpdateSpeed(0.5f);
                    StartCoroutine(ResetEnemySpeed(em));
                }
                CarEnemy cm = hit.transform.GetComponent<CarEnemy>();
                    if (cm != null) {
                    cm.UpdateSpeed(0.5f);
                    StartCoroutine(ResetEnemySpeed1(cm));
                }
                SpiderEnemy sm = hit.transform.GetComponent<SpiderEnemy>();
                if (sm != null) {
                    sm.UpdateSpeed(0.5f);
                    StartCoroutine(ResetEnemySpeed2(sm));
                }

                BossMovement bm = hit.transform.GetComponent<BossMovement>();
                if (bm != null) {
                    bm.UpdateSpeed(0.5f);
                    StartCoroutine(ResetEnemySpeed3(bm));
                }

                WitchEnemy wm = hit.transform.GetComponent<WitchEnemy>();
                if (wm != null) {
                    wm.UpdateSpeed(0.5f);
                    StartCoroutine(ResetEnemySpeed4(wm));
                }
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em) {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }

     private IEnumerator ResetEnemySpeed1(CarEnemy cm) {
        yield return new WaitForSeconds(freezeTime);

        cm.ResetSpeed();
    }

    private IEnumerator ResetEnemySpeed2(SpiderEnemy sm) {
        yield return new WaitForSeconds(freezeTime);

        sm.ResetSpeed();
    }

        private IEnumerator ResetEnemySpeed3(BossMovement bm) {
        yield return new WaitForSeconds(freezeTime);

        bm.ResetSpeed();
    }

        private IEnumerator ResetEnemySpeed4(WitchEnemy wm) {
        yield return new WaitForSeconds(freezeTime);

        wm.ResetSpeed();
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
        LevelManager.main.gold += towerSellCost;
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
