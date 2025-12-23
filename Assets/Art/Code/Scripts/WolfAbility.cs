using UnityEngine;

public class WolfAbility : Turret
{
    [Header("Wolf Ability Settings")]
    [SerializeField] private int attacksForBite = 4;          // Každý kolikátý útok je bite
    [SerializeField] private float biteDamageMultiplier = 2f; // 2× damage
    [SerializeField] private GameObject biteEffectPrefab;     // Speciální VFX pro bite

    private int attackCounter = 0;

    protected override void DoMeleeAttack(int damage)
    {
        attackCounter++;

        bool isBite = attackCounter >= attacksForBite;

        if (isBite)
        {
            attackCounter = 0;

            int biteDamage = Mathf.RoundToInt(damage * biteDamageMultiplier);

            // Bite VFX (unikátní)
            if (biteEffectPrefab != null)
            {
                Instantiate(biteEffectPrefab, target.position, Quaternion.identity);
            }

            // Damage bez normálního melee efektu
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(biteDamage, DamageType.Physical);
            }
        }
        else
        {
            // Normální melee útok (včetně default VFX)
            base.DoMeleeAttack(damage);
        }
    }
}
