using System.Collections;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private int skeletonCount = 1;
    [SerializeField] private float spawnRadius = 1.5f;

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 5f; // čas mezi spawny
    [SerializeField] private float stopDuration = 1f; // jak dlouho se Necromancer zastaví při spawnování

    private EnemyMovement necroMovement;

    private void Start()
    {
        necroMovement = GetComponent<EnemyMovement>();

        // Spouštíme periodický spawn
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            yield return StartCoroutine(SpawnWithStop());
        }
    }

    private IEnumerator SpawnWithStop()
    {
        if (necroMovement != null)
        {
            // zastavení Necromancera
            necroMovement.UpdateSpeed(0f);
        }

        // počkej stopDuration, aby byl zastaven
        yield return new WaitForSeconds(stopDuration);

        SpawnSkeletons();

        // obnov původní rychlost
        if (necroMovement != null)
        {
            necroMovement.ResetSpeed();
        }
    }

    private void SpawnSkeletons()
    {
        if (skeletonPrefab == null) return;

        int cartPathIndex = 0;
        if (necroMovement != null)
            cartPathIndex = necroMovement.GetPathIndex();

        for (int i = 0; i < skeletonCount; i++)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Random.Range(0.5f, spawnRadius);
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            Vector3 spawnPos = transform.position + (Vector3)offset;

            GameObject newSkeleton = Instantiate(skeletonPrefab, spawnPos, Quaternion.identity);

            // nastavení pathIndex podle Necromancera
            EnemyMovement em = newSkeleton.GetComponent<EnemyMovement>();
            if (em != null)
            {
                em.SetPathIndex(cartPathIndex);
            }
        }
    }
}

