using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement Instance {get; private set;}
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int hpDamage = 2;
    [SerializeField] private int xRotate;
    [SerializeField] private int yRotate;
    [SerializeField] private int zRotate;

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
                return;
            } 
            else 
            {
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
            transform.rotation = Quaternion.Euler(xRotate, yRotate, zRotate);
        }
    }
}