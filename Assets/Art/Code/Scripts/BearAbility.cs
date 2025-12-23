using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAbility : Turret
{
    [Header("Special Ability Settings")]
    [SerializeField] private float abilityCharge = 0f;               // Nabíjení schopnosti 0–100
    [SerializeField] private float abilityChargePerDamage = 0.5f;   // Kolik % nabití za 1 damage
    [SerializeField] private float abilityRange = 3f;                // Radius schopnosti
    [SerializeField] private float abilityDuration = 2f;             // Jak dlouho nepřátelé stojí
    [SerializeField] private GameObject abilityEffectPrefab;         // Volitelný vizuální efekt

    protected override void Shoot()
    {
        base.Shoot(); // vykoná standardní střelbu z Turret

        // Přidání nabití speciální schopnosti podle způsobeného damage
        int damage = CalculateOutputDamage();
        Debug.Log("Shoot called, damage: " + damage);
        Debug.Log("Damage: " + damage + ", Charge added: " + damage * abilityChargePerDamage);
    }

    // Přepsaná metoda DoMeleeAttack() – po úderu se přidá charge
    protected override void DoMeleeAttack(int damage)
    {
        base.DoMeleeAttack(damage);
        AddCharge(damage);
    }

    // Přidání charge podle způsobeného damage
    private void AddCharge(int damage)
    {
        abilityCharge += damage * abilityChargePerDamage;
        if (abilityCharge >= 100f)
        {
            abilityCharge = 0f;
            ActivateAbility();
        }
    }

    private void ActivateAbility()
    {
        if (abilityEffectPrefab != null)
        {
            Instantiate(abilityEffectPrefab, transform.position, Quaternion.identity);
        }

    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, abilityRange, enemyMask);
    foreach (Collider2D enemy in enemies)
    {
        EnemyMovement em = enemy.GetComponent<EnemyMovement>();
        if (em != null)
            em.Stun(abilityDuration); // použije novou metodu Stun
    }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, abilityRange);
    }
}
