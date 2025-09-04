using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health2 : MonoBehaviour, IDamageable {
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] private int enemyScore;

    [Header("References")]
    [SerializeField] private AudioClip death;

    [Header("Spawner Settings")]
    [Tooltip("Označit, pokud tento nepřítel pochází z druhé cesty/spawneru")]
    [SerializeField] private bool isSecondPath = false;

    private bool isDestroyed = false;

    public void TakeDamage(int amount) {
        hitPoints -= amount;

        if (hitPoints <= 0 && !isDestroyed)
        {
            if (isSecondPath)
            {
                EnemySpawnerB.onEnemyDestroy2.Invoke();
                LevelManager.main.IncreaseCurrency(currencyWorth);
                isDestroyed = true;
                Destroy(gameObject);
                LevelManager.main.score = LevelManager.main.score + enemyScore;
                AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, 0.1f);
            }
            else
            {
                EnemySpawnerB.onEnemyDestroy.Invoke();
                LevelManager.main.IncreaseCurrency(currencyWorth);
                isDestroyed = true;
                Destroy(gameObject);
                LevelManager.main.score = LevelManager.main.score + enemyScore;
                AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, 0.1f);
            }
        }
    }
} 



    

