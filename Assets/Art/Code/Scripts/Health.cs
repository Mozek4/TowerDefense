using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] private int enemyScore;

    [Header("References")]
    [SerializeField] private AudioClip death;
/*     [SerializeField] public int BaseHitPoints; */
    private bool isDestroyed = false;

/*     private void Awake() {
        ResetHealth();
    } */

    public void TakeDamage(int dmg) {
        hitPoints -= dmg;
        //Debug.Log(hitPoints);

        if (hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
            LevelManager.main.score = LevelManager.main.score + enemyScore;
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, 0.1f);
        }
    }
/*     private void ResetHealth() {
        hitPoints = BaseHitPoints;
    } */
} 



    

