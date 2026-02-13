using UnityEngine;
using UnityEngine.UI;

public class BearAbility : Turret
{
    [Header("UI Settings")]
    [SerializeField] private Slider chargeSlider;

    [Header("Special Ability Settings")]
    [SerializeField] private float abilityCharge = 0f;
    [SerializeField] private float abilityChargePerDamage = 0.5f;
    [SerializeField] private float abilityRange = 3f;
    [SerializeField] private float abilityDuration = 2f;
    [SerializeField] private GameObject abilityEffectPrefab;

    protected override void Start()
    {
        base.Start();

        if (chargeSlider != null)
        {
            chargeSlider.maxValue = 100f;
            chargeSlider.value = abilityCharge;
        }
    }

    protected override void Shoot()
    {
        base.Shoot(); 
        int damage = CalculateOutputDamage();
        AddCharge(damage);
    }

    protected override void DoMeleeAttack(int damage)
    {
        base.DoMeleeAttack(damage);
        AddCharge(damage);
    }

    private void AddCharge(int damage)
    {
        abilityCharge += damage * abilityChargePerDamage;

        if (chargeSlider != null)
        {
            chargeSlider.value = abilityCharge;
        }

        if (abilityCharge >= 100f)
        {
            abilityCharge = 0f;
            if (chargeSlider != null) chargeSlider.value = 0f;
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
                em.Stun(abilityDuration);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, abilityRange);
    }
}