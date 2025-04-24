using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ElectroSpiritMovement : MonoBehaviour
{
    public static EnemyMovement Instance {get; private set;}
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float floatLength = 0.2f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private int hpDamage = 2;

    private Transform target;
    private int pathIndex = 0;
    private float baseSpeed;
    private float startY;

    private void Start() {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
        startY = transform.position.y;
    }

    private void Update() {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length) {
                LevelManager.playerHealthReduce(hpDamage);
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                //Debug.Log(LevelManager.playerHealth);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
                RotateByDirection();
            }
        }
    }
    private void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;

        if (pathIndex != 6) {
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatLength; 
            rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed + yOffset);
        }
        else {
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatLength; 
            rb.velocity = new Vector2(direction.x * moveSpeed + yOffset, direction.y * moveSpeed);
        }
    }

    public void UpdateSpeed(float newSpeed) {
            moveSpeed = newSpeed;
    }

    public void ResetSpeed() {
        moveSpeed = baseSpeed;
    }

    private void RotateByDirection() {
        if (pathIndex == 6) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
