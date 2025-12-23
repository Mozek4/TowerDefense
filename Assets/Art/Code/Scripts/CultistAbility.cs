using UnityEngine;
using System.Collections;

public class Cultist : MonoBehaviour
{
    [Header("Heal Settings")]
    public int healAmount = 10;
    public float healRange = 3f;
    public float healCooldown = 4f;

    private float healTimer;

    [Header("Layers")]
    public LayerMask enemyLayer;

    [Header("Heal Effect")]
    [SerializeField] private GameObject healEffectPrefab;
    [SerializeField] private float effectHeightOffset = 1.2f;
    [SerializeField] private float effectMoveUpDistance = 0.8f;
    [SerializeField] private float effectDuration = 1f;
    [SerializeField] private float effectVisualScale = 0.3f;


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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, healRange, enemyLayer);

        foreach (Collider2D col in enemies)
        {
            if (col.gameObject == this.gameObject)
                continue;

            Health health = col.GetComponent<Health>();

            if (health == null)
                continue;

            // ⚠️ kontrola, jestli má smysl healovat
            if (health.hitPoints >= health.maxHP)
                continue;

            int beforeHeal = health.hitPoints;
            health.TakeHeal(healAmount);

            // pokud se skutečně přidaly životy
            if (health.hitPoints > beforeHeal)
            {
                SpawnHealEffect(col.transform);
            }
        }
    }
    private void SpawnHealEffect(Transform target)
    {
        if (healEffectPrefab == null)
            return;

        GameObject effect = Instantiate(healEffectPrefab);
        effect.transform.SetParent(target);

        effect.transform.localScale = Vector3.one * effectVisualScale;
        effect.transform.localPosition = Vector3.up * effectHeightOffset;

        StartCoroutine(HealEffectCoroutine(effect));
    }


    private IEnumerator HealEffectCoroutine(GameObject effect)
    {
        if (effect == null)
            yield break;

        SpriteRenderer sr = effect.GetComponentInChildren<SpriteRenderer>();

        if (sr == null)
            yield break;

        Color startColor = sr.color;

        Vector3 startPos = effect.transform.localPosition;
        Vector3 endPos = startPos + Vector3.up * effectMoveUpDistance;

        float timer = 0f;

        while (timer < effectDuration)
        {
            if (effect == null)
                yield break;

            timer += Time.deltaTime;
            float t = timer / effectDuration;

            effect.transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            Color c = startColor;
            c.a = Mathf.Lerp(1f, 0f, t);
            sr.color = c;

            yield return null;
        }

        Destroy(effect);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRange);
    }
}
