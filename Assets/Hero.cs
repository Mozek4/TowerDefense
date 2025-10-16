using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroController : MonoBehaviour
{
    [SerializeField] private LineRenderer rangeIndicator;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float stopDistance = 0.1f; 
    private Vector2 targetPosition;
    private bool isMoving = false;

    [Header("Attack")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1.0f;
    public int attackDamage = 1;   
    private float attackTimer = 0f;

    [Header("Detection")]
    public LayerMask enemyLayerMask; 

    private Rigidbody2D rb;
    private bool awaitingMoveCommand = false;
    private Transform currentTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;

        if (rangeIndicator != null)
        {
            rangeIndicator.positionCount = 0;
            rangeIndicator.enabled = false;
            rangeIndicator.useWorldSpace = true; // nutné, aby bylo kolem hrdiny
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 pos = rb.position;
            float dist = Vector2.Distance(pos, targetPosition);
            if (dist <= stopDistance)
            {
                isMoving = false;
            }
            else
            {
                Vector2 newPos = Vector2.MoveTowards(pos, targetPosition, moveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                // otočení podle směru pohybu
                Vector3 scale = transform.localScale;
                if (targetPosition.x > pos.x && scale.x < 0)
                {
                    scale.x *= -1;
                }
                else if (targetPosition.x < pos.x && scale.x > 0)
                {
                    scale.x *= -1;
                }
                transform.localScale = scale;
            }
        }
    }

    void Update()
    {
        HandleMoveCommandInput();

        // aktualizace pozice kruhu, pokud je aktivní
        if (rangeIndicator.enabled)
        {
            DrawRangeCircle();
        }

        // Útok jen pokud stojí
        if (!isMoving)
        {
            attackTimer -= Time.deltaTime;
            AcquireAndAttack();
        }
        else
        {
            currentTarget = null;
        }
    }

private void HandleMoveCommandInput()
{
    // pokud čekám na cíl a kliknu
    if (awaitingMoveCommand && Input.GetMouseButtonDown(0))
    {
        // ✳️ KONTROLA: jestli klik nebyl přes UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return; // ignoruj klik, pokud byl přes UI tlačítko
        }

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = new Vector2(mouseWorld.x, mouseWorld.y);
        isMoving = true;
        awaitingMoveCommand = false;

        HideRangeCircle(); // po kliknutí vypni kruh
    }
}

    // volá se z UI Button
    public void StartMoveCommand()
    {
        // když už čekám na příkaz, vypni režim
        if (awaitingMoveCommand)
        {
            awaitingMoveCommand = false;
            HideRangeCircle();
        }
        else
        {
            awaitingMoveCommand = true;
            ShowRangeCircle();
        }
    }

    private void AcquireAndAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayerMask);
        if (hits.Length == 0) return;

        Transform nearest = null;
        float bestDist = float.MaxValue;
        Vector2 myPos = transform.position;
        foreach (var c in hits)
        {
            float d = Vector2.SqrMagnitude((Vector2)c.transform.position - myPos);
            if (d < bestDist)
            {
                bestDist = d;
                nearest = c.transform;
            }
        }

        if (nearest == null) return;

        currentTarget = nearest;

        if (attackTimer <= 0f)
        {
            DoAttack(currentTarget);
            attackTimer = attackCooldown;
        }
    }

    private void DoAttack(Transform target)
    {
        var enemyHealth = target.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
        }
    }

    // === RANGE KRUH ===

    private void ShowRangeCircle()
    {
        if (rangeIndicator == null) return;

        rangeIndicator.enabled = true;
        rangeIndicator.sortingLayerName = "UI"; // nebo jiná, podle hry
        rangeIndicator.sortingOrder = 10;
        DrawRangeCircle();
    }

    private void HideRangeCircle()
    {
        if (rangeIndicator == null) return;

        rangeIndicator.enabled = false;
        rangeIndicator.positionCount = 0;
    }

    private void DrawRangeCircle()
    {
        if (rangeIndicator == null || !rangeIndicator.enabled) return;

        int segments = 50;
        float angleStep = 360f / segments;
        Vector3[] positions = new Vector3[segments + 1];

        Vector3 center = transform.position;
        center.z = -0.1f;

        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            float x = Mathf.Cos(angle) * attackRange + center.x;
            float y = Mathf.Sin(angle) * attackRange + center.y;
            positions[i] = new Vector3(x, y, center.z);
        }

        rangeIndicator.positionCount = positions.Length;
        rangeIndicator.SetPositions(positions);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
