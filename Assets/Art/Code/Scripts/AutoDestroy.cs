using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] private float lifeTime = 0.25f;
    [SerializeField] private float fadeDuration = 0.15f;

    [Header("Scale")]
    [SerializeField] private Vector3 startScale = Vector3.one * 0.6f;
    [SerializeField] private Vector3 endScale = Vector3.one * 1.1f;

    private SpriteRenderer spriteRenderer;
    private float timer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = startScale;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / lifeTime);

        // Scale
        transform.localScale = Vector3.Lerp(startScale, endScale, t);

        // Fade
        if (timer >= lifeTime - fadeDuration)
        {
            float fadeT = (timer - (lifeTime - fadeDuration)) / fadeDuration;
            Color c = spriteRenderer.color;
            c.a = Mathf.Lerp(1f, 0f, fadeT);
            spriteRenderer.color = c;
        }

        // Destroy
        if (timer >= lifeTime)
            Destroy(gameObject);
    }
}
