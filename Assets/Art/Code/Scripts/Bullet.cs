using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] public int bulletDamage = 1;

    [Header("Damage Type")]
    public DamageType damageType = DamageType.Physical;

    [Header("Behavior Settings")]
    [SerializeField] private bool tipRotation = false;

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
                float finalDamage = bulletDamage;

                // Typ nepřítele
                if (damageType == DamageType.Magic && health.enemyType == EnemyType.Undead)
                {
                    finalDamage *= 1.5f;
                }

                // Upgrady z PlayerStats
                if (PlayerStats.instance != null)
                {
                    finalDamage *= PlayerStats.instance.towerDamageMultiplier;
                }

                // Dark Hag shield
                EnemyDamageModifier edm = collision.gameObject.GetComponent<EnemyDamageModifier>();
                if (edm != null)
                {
                    finalDamage = edm.ApplyDamage(Mathf.RoundToInt(finalDamage));
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

    // Public getter pro případ Turret.cs
    public int Damage => bulletDamage;
}

public enum DamageType
{
    Physical,
    Magic
}
