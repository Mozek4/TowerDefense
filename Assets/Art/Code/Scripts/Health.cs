using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] private int enemyScore;

    [Header("Enemy Type")]
    public EnemyType enemyType = EnemyType.Casual;

    [Header("References")]
    [SerializeField] private AudioClip death;
    private bool isDestroyed = false;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
            LevelManager.main.score = LevelManager.main.score + enemyScore;
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, 0.1f);
            Debug.Log(LevelManager.main.score);
        }
    }
}

public enum EnemyType
{
    Casual,
    Undead
}




    

