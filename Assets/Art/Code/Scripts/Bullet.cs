using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;
    private float bulletTimeToLive = 2.5f;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    private void FixedUpdate() {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform == target) {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
    }
/*         if (collision.transform == target) {
                Health2 health = collision.gameObject.GetComponent<Health2>();
                if (health != null) {
                    health.TakeDamage(bulletDamage);
                }
                Destroy(gameObject);
                } */
    }
    private void Start () {
        Destroy (gameObject, bulletTimeToLive);
    }
}
