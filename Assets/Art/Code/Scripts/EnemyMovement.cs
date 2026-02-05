using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int hpDamage = 2;

    [Header("Behavior Settings")]
    [SerializeField] private bool rotationByDirection = false; // ✅ Checkbox v inspektoru

    private Transform target;
    private int pathIndex = 0;
    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];

        if (!rotationByDirection)
            RotateByFixedDirection();
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                LevelManager.playerHealthReduce(hpDamage);
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];

                // ✅ Pokud má mít rotaci podle směru, otočí se
                if (rotationByDirection)
                {
                    Vector2 direction = (target.position - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, angle - 270);
                }
                else
                {
                    // ⚙️ Alternativní pevná rotace, pokud to chceš zachovat pro určité případy
                    RotateByFixedDirection();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        float oldSpeed = moveSpeed;
        moveSpeed = 0f;           // zastaví nepřítele
        yield return new WaitForSeconds(duration);
        moveSpeed = oldSpeed;     // obnoví původní rychlost
    }
    private void RotateByFixedDirection()
    {
        if (LevelManager.main == null || LevelManager.main.path == null)
            return;

        Transform[] path = LevelManager.main.path;

        if (path.Length < 2)
            return;

        Vector2 chosenDir;

        // 🔹 SPECIÁLNÍ PŘÍPAD: začátek cesty
        if (pathIndex == 0)
        {
            chosenDir = (Vector2)(path[1].position - path[0].position);
        }
        else
        {
            int nextIndex = pathIndex;
            if (nextIndex >= path.Length)
                return;

            Vector2 dirToNext = path[nextIndex].position - transform.position;
            chosenDir = dirToNext;

            // pokud je segment vertikální, koukni na další
            if (Mathf.Abs(dirToNext.y) > Mathf.Abs(dirToNext.x))
            {
                int afterIndex = nextIndex + 1;
                if (afterIndex < path.Length)
                {
                    chosenDir = (Vector2)(path[afterIndex].position - path[nextIndex].position);
                }
            }
        }

        if (chosenDir.x == 0)
            return;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(chosenDir.x) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

    // Getter pro získání aktuálního pathIndexu
    public int GetPathIndex()
    {
        return pathIndex;
    }

    // Setter pro nastavení pathIndexu (a aktualizaci cílového waypointu)
    public void SetPathIndex(int index)
    {
        pathIndex = index;

        // nastav nový target podle indexu
        if (pathIndex < LevelManager.main.path.Length)
            target = LevelManager.main.path[pathIndex];
    }
}
