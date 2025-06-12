using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
public class GuardianTree : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint1;
    [SerializeField] private Transform firingPoint2;

    [Header("Attribute")]
    [SerializeField] private float range = 5f;
    [SerializeField] private float aps = 1f;

    private Transform target1;
    private Transform target2;
    private float timeUntilFire;
    public GameObject bulletObj1;
    public GameObject bulletObj2;
    
    private void Update() {
        if (target1 == null) {
            FindFirstTarget();
        }
        if (target2 == null || target2 == target1) {
            FindSecondTarget();
        }

        if (target1 != null && !CheckFirstTargetIsInRange()) {
        target1 = null;
        }
        if (target2 != null && !CheckSecondTargetIsInRange()) {
        target2 = null;
        }

        timeUntilFire += Time.deltaTime;
        
            if (timeUntilFire >= 1f/aps) {
                Shoot();
                timeUntilFire = 0f;
            }  
    }
    private void Shoot() {
        if (target1 != null) {
            GameObject bulletObj1 = Instantiate(bulletPrefab, firingPoint1.position, Quaternion.identity);
            Bullet bulletScript1 = bulletObj1.GetComponent<Bullet>();
            bulletScript1.SetTarget(target1);
        }
        if (target2 != null) {
            GameObject bulletObj2 = Instantiate(bulletPrefab, firingPoint2.position, Quaternion.identity);
            Bullet bulletScript2 = bulletObj2.GetComponent<Bullet>();
            bulletScript2.SetTarget(target2);
        }
    }

    private void FindFirstTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0f, enemyMask);

        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].transform != target2) {
            target1 = hits[i].transform;
            break;
            }
        }
    }
    private void FindSecondTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0f, enemyMask);

        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].transform != target1) {
            target2 = hits[i].transform;
            break;
            }
        }
    }
    private bool CheckFirstTargetIsInRange() {
        return target1 != null && Vector2.Distance(target1.position, transform.position) <= range;
    }

    private bool CheckSecondTargetIsInRange() {
            return target2 != null && Vector2.Distance(target2.position, transform.position) <= range;
    }
}
