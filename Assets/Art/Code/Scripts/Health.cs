using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;
/*     [SerializeField] public int BaseHitPoints; */
    private bool isDestroyed = false;

/*     private void Awake() {
        ResetHealth();
    } */

    public void TakeDamage(int dmg) {
        hitPoints -= dmg;
        Debug.Log(hitPoints);

        if (hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
/*     private void ResetHealth() {
        hitPoints = BaseHitPoints;
    } */
} 



    

