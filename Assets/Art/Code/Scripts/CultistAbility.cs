using UnityEngine;

public class Cultist : MonoBehaviour
{
    [Header("Heal Settings")]
    public int healAmount = 10;
    public float healRange = 3f;
    public float healCooldown = 4f;

    private float healTimer;

    [Header("Layers")]
    public LayerMask enemyLayer;

    void Start()
    {
        healTimer = healCooldown;
    }

    void Update()
    {
        healTimer -= Time.deltaTime;

        if (healTimer <= 0f)
        {
            HealEnemiesInRange();
            healTimer = healCooldown;
        }
    }

    private void HealEnemiesInRange()
    {
        // Najdi všechny enemies v okolí
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, healRange, enemyLayer);

        foreach (Collider2D col in enemies)
        {
            // IGNORUJE sám sebe
            if (col.gameObject == this.gameObject) 
                continue;

            // Vezmeme Health místo Enemy
            Health health = col.GetComponent<Health>();

            if (health != null)
            {
                health.TakeHeal(healAmount);
            }
        }
    }

    // Debug vizualizace
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRange);
    }
}

