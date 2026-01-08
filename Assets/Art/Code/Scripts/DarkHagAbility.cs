using System.Collections.Generic;
using UnityEngine;

public class DarkHagAbility : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField] private float range = 5f;
    [SerializeField] private float shieldReduction = 0.3f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Visual Settings")]
    [SerializeField] private float effectSize = 3f;
    [SerializeField] private Color effectColor = new Color(0.6f, 0f, 0.8f, 0.35f);

    private HashSet<EnemyDamageModifier> affectedEnemies = new HashSet<EnemyDamageModifier>();
    private Dictionary<EnemyDamageModifier, GameObject> activeEffects =
        new Dictionary<EnemyDamageModifier, GameObject>();

    private Sprite circleSprite;

    private void Awake()
    {
        circleSprite = CreateCircleSprite(128);
    }

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        List<EnemyDamageModifier> toRemove = new List<EnemyDamageModifier>();

        foreach (var edm in affectedEnemies)
        {
            if (edm == null || !IsInRange(edm.gameObject))
            {
                if (edm != null)
                {
                    edm.RemoveTemporaryShield(shieldReduction);

                    if (activeEffects.ContainsKey(edm))
                    {
                        Destroy(activeEffects[edm]);
                        activeEffects.Remove(edm);
                    }
                }

                toRemove.Add(edm);
            }
        }

        foreach (var edm in toRemove)
            affectedEnemies.Remove(edm);

        foreach (var hit in hits)
        {
            EnemyDamageModifier edm = hit.GetComponent<EnemyDamageModifier>();

            if (edm != null && !affectedEnemies.Contains(edm))
            {
                edm.AddTemporaryShield(shieldReduction);
                affectedEnemies.Add(edm);

                GameObject effect = CreateShieldEffect(edm.transform);
                activeEffects.Add(edm, effect);
            }
        }
    }

    private bool IsInRange(GameObject obj)
    {
        return Vector2.Distance(transform.position, obj.transform.position) <= range;
    }

    private GameObject CreateShieldEffect(Transform target)
    {
        GameObject effect = new GameObject("ShieldEffect");
        effect.transform.SetParent(target);
        effect.transform.localPosition = Vector3.zero;
        effect.transform.localScale = Vector3.one * effectSize;

        SpriteRenderer sr = effect.AddComponent<SpriteRenderer>();
        sr.sprite = circleSprite;
        sr.color = effectColor;
        SpriteRenderer enemyRenderer = target.GetComponent<SpriteRenderer>();

        if (enemyRenderer != null)
        {
            sr.sortingLayerID = enemyRenderer.sortingLayerID;
            sr.sortingOrder = enemyRenderer.sortingOrder - 1;
        }
        else
        {
            sr.sortingLayerName = "Default";
            sr.sortingOrder = -1;
        }

        return effect;
    }

    private Sprite CreateCircleSprite(int size)
    {
        Texture2D tex = new Texture2D(size, size);
        tex.filterMode = FilterMode.Bilinear;

        Color clear = new Color(0, 0, 0, 0);
        Vector2 center = new Vector2(size / 2f, size / 2f);
        float radius = size / 2f;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center);
                tex.SetPixel(x, y, dist <= radius ? Color.white : clear);
            }
        }

        tex.Apply();

        return Sprite.Create(
            tex,
            new Rect(0, 0, size, size),
            new Vector2(0.5f, 0.5f),
            16f
        );
    }

    private void OnDestroy()
    {
        // Když DarkHag zmizí, uklidíme všechny shielde a efekty
        foreach (var edm in affectedEnemies)
        {
            if (edm != null)
            {
                edm.RemoveTemporaryShield(shieldReduction);
            }

            if (edm != null && activeEffects.ContainsKey(edm))
            {
                Destroy(activeEffects[edm]);
            }
        }

        affectedEnemies.Clear();
        activeEffects.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.5f, 0f, 0.5f, 0.3f);
        Gizmos.DrawSphere(transform.position, range);
    }
}
