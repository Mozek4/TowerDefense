using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTree : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint1;
    [SerializeField] private Transform firingPoint2;

    [Header("Attributes")]
    [SerializeField] private float range = 5f;
    [SerializeField] private float aps = 1f;
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private DamageType damageType = DamageType.Physical;

    private Transform target1;
    private Transform target2;
    private float timeUntilFire;

    private void Update()
    {
        if (target1 == null) FindFirstTarget();
        if (target2 == null || target2 == target1) FindSecondTarget();

        if (target1 != null && Vector2.Distance(target1.position, transform.position) > range)
            target1 = null;
        if (target2 != null && Vector2.Distance(target2.position, transform.position) > range)
            target2 = null;

        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / aps)
        {
            Shoot();
            timeUntilFire = 0f;
        }
    }

    private void Shoot()
    {
        if (target1 != null)
            DealDamage(target1);

        if (target2 != null)
            DealDamage(target2);
    }

    private void DealDamage(Transform target)
    {
        if (target == null) return;

        // efekt střely
        if (bulletPrefab != null)
            Instantiate(bulletPrefab, firingPoint1.position, Quaternion.identity);

        Health health = target.GetComponent<Health>();
        if (health != null)
            health.TakeDamage(baseDamage, damageType);
    }

    private void FindFirstTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0f, enemyMask);
        foreach (var hit in hits)
        {
            if (hit.transform != target2)
            {
                target1 = hit.transform;
                break;
            }
        }
    }

    private void FindSecondTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0f, enemyMask);
        foreach (var hit in hits)
        {
            if (hit.transform != target1)
            {
                target2 = hit.transform;
                break;
            }
        }
    }
}
