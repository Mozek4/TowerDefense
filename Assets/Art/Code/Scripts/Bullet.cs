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

    [Header("Behavior Settings")]
    [SerializeField] private bool tipRotation = false; // ✅ Checkbox pro zapnutí/vypnutí rotace

    private Transform target;
    private float bulletTimeToLive = 2.5f;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        // směr k cíli
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        // ✅ pokud je zaškrtnutý tipRotation, otáčí se projektil směrem k cíli
        if (tipRotation)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 180));
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

                // 1️⃣ Bonus podle typu nepřítele
                if (damageType == DamageType.Magic && health.enemyType == EnemyType.Undead)
                {
                    finalDamage *= 1.5f; // +50 % proti undeadům
                }

                // 2️⃣ Upgrady z PlayerStats
                finalDamage *= PlayerStats.instance.towerDamageMultiplier;

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
