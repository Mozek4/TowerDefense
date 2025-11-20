using System.Collections.Generic;
using UnityEngine;

public class DarkHagAbility : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField] private float range = 5f;               // radius aury
    [SerializeField] private float shieldReduction = 0.3f;   // 30% snížení damage

    // Uchovává nepřátele, kteří jsou momentálně chráněni
    private HashSet<EnemyDamageModifier> affectedEnemies = new HashSet<EnemyDamageModifier>();

    private void Update()
    {
        // Najdeme všechny nepřátele v oblasti
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);

        // Odstraníme shield těm, kteří už nejsou v range nebo byli zničeni
        List<EnemyDamageModifier> toRemove = new List<EnemyDamageModifier>();
        foreach (var edm in affectedEnemies)
        {
            if (edm == null || !IsInRange(edm.gameObject))
            {
                if (edm != null)
                    edm.RemoveTemporaryShield(shieldReduction);
                toRemove.Add(edm);
            }
        }
        foreach (var edm in toRemove)
        {
            affectedEnemies.Remove(edm);
        }

        // Přidáme shield těm, kteří jsou v range a ještě nejsou chráněni
        foreach (var hit in hits)
        {
            EnemyDamageModifier edm = hit.GetComponent<EnemyDamageModifier>();
            if (edm != null && !affectedEnemies.Contains(edm))
            {
                edm.AddTemporaryShield(shieldReduction);
                affectedEnemies.Add(edm);
            }
        }
    }

    private bool IsInRange(GameObject obj)
    {
        return Vector2.Distance(transform.position, obj.transform.position) <= range;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.5f, 0f, 0.5f, 0.3f);
        Gizmos.DrawSphere(transform.position, range);
    }
}
