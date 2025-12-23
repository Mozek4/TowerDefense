using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private bool tipRotation = false;

    // Payload data (co kulka nese)
    private int damagePayload;
    private DamageType damageType;
    
    private Transform target;
    private float bulletTimeToLive = 2.5f;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Hlavní metoda pro nastavení kulky z Věže
    public void SetupBullet(int damage, DamageType type)
    {
        this.damagePayload = damage;
        this.damageType = type;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        if (tipRotation)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 180);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == target)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                // Předáme damage a typ přímo do Health. 
                // Žádné výpočty PlayerStats nebo Shieldů zde!
                health.TakeDamage(damagePayload, damageType);
            }

            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, bulletTimeToLive);
    }
}

// Enum necháme zde, aby byl dostupný
public enum DamageType
{
    Physical,
    Magic
}