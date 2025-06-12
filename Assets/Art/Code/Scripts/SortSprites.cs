using UnityEngine;

public class SortSprites : MonoBehaviour
{
    public SpriteRenderer[] extraRenderers;
    public int offset = 1;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        int baseOrder = Mathf.RoundToInt(transform.position.y * -100);
        sr.sortingOrder = baseOrder;

        foreach (var extra in extraRenderers)
        {
            if (extra != null)
                extra.sortingOrder = baseOrder + offset;
        }
    }
}
