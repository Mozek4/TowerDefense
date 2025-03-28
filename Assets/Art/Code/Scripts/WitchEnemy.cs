using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchEnemy : MonoBehaviour
{
    public static WitchEnemy Instance {get; private set;}
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int hpDamage = 2;

    private Transform target;
    private int pathIndex = 0;
    private float baseSpeed;

    private void Start() {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
    }

    private void Update() {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length) {
                LevelManager.playerHealthReduce(hpDamage);
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                Debug.Log(LevelManager.playerHealth);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
                RotateByDirection();
            }
        }
    }
    private void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed) {
            moveSpeed = newSpeed;
    }

    public void ResetSpeed() {
        moveSpeed = baseSpeed;
    }

    private void RotateByDirection() {
        if (pathIndex == 6) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
