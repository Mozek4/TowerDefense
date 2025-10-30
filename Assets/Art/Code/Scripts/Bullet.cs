using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    [Header("Damage Type")]
    public DamageType damageType = DamageType.Physical;

    private Transform target;
    private float bulletTimeToLive = 2.5f;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == target)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                float finalDamage = bulletDamage;

                // Magic deals 1.5x damage to Undead enemies
                if (damageType == DamageType.Magic && health.enemyType == EnemyType.Undead)
                {
                    finalDamage *= 1.5f;
                }

                health.TakeDamage(Mathf.RoundToInt(finalDamage));
            }

            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, bulletTimeToLive);
    }
}

public enum DamageType
{
    Physical,
    Magic
}
