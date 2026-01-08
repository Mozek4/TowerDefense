using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 2;
    [SerializeField] public int maxHP = 2;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] private int enemyScore;

    [Header("Enemy Type")]
    public EnemyType enemyType = EnemyType.Casual;

    [Header("References")]
    //[SerializeField] private AudioClip death;
    
    private bool isDestroyed = false;
    private EnemyDamageModifier damageModifier; // Reference na štíty

    private void Start()
    {
        // Cachujeme referenci na modifikátor (shieldy), pokud existuje
        damageModifier = GetComponent<EnemyDamageModifier>();
    }

    // HLAVNÍ METODA PRO PŘÍJEM POŠKOZENÍ
    public void TakeDamage(int rawDamage, DamageType incomingType)
    {
        float calculatedDamage = rawDamage;

        // 1. Logika elementálních slabin (Magic vs Undead)
        // Toto patří sem, protože to závisí na typu nepřítele
        if (incomingType == DamageType.Magic && enemyType == EnemyType.Undead)
        {
            calculatedDamage *= 1.5f;
        }

        // 2. Aplikace štítů (Dark Hag / EnemyDamageModifier)
        if (damageModifier != null)
        {
            calculatedDamage = damageModifier.ApplyDamageReduction(calculatedDamage);
        }

        // 3. Finální odečtení
        int finalDamage = Mathf.RoundToInt(calculatedDamage);
        if (finalDamage < 0) finalDamage = 0; // Pojistka

        ApplyDamageInternal(finalDamage);
    }

    // Overload pro případy, kdy se volá damage bez typu (např. pasti) -> bere se jako Physical
    public void TakeDamage(int dmg)
    {
        TakeDamage(dmg, DamageType.Physical);
    }

    private void ApplyDamageInternal(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            Die();
        }
    }

    private void Die()
    {
        isDestroyed = true;
        EnemySpawner.onEnemyDestroy.Invoke();
        LevelManager.main.IncreaseCurrency(currencyWorth);
        LevelManager.main.score = LevelManager.main.score + enemyScore;

        // SPUŠTĚNÍ VŠECH SCHOPNOSTÍ PO SMRTI
        OnDeathAbility[] abilities = GetComponents<OnDeathAbility>();
        foreach (var ability in abilities)
        {
            ability.Activate();
        }

        //AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, 0.1f);
        Debug.Log(LevelManager.main.score);
        Destroy(gameObject);
    }

    public void TakeHeal(int amount)
    {
        hitPoints += amount;

        if (hitPoints > maxHP)
            hitPoints = maxHP;
    }
}

public enum EnemyType
{
    Casual,
    Undead
}