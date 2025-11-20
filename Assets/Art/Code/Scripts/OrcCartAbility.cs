using UnityEngine;

public class OnDeathSpawnOrcs : OnDeathAbility
{
    [Header("Spawn Settings")]
    public GameObject smallOrcPrefab;
    public int spawnCount = 3;
    public float spawnRadius = 1.5f;

    public override void Activate()
    {
        // Získáme EnemyMovement cartu (toho, kdo spouští schopnost)
        EnemyMovement cartMovement = GetComponent<EnemyMovement>();
        int cartPathIndex = 0;

        if (cartMovement != null)
        {
            cartPathIndex = cartMovement.GetPathIndex();
        }

        for (int i = 0; i < spawnCount; i++)
        {
            // Náhodný úhel v radiánech
            float angle = Random.Range(0f, Mathf.PI * 2);

            // Náhodná vzdálenost od těla
            float distance = Random.Range(0.5f, spawnRadius);

            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            Vector3 spawnPos = transform.position + (Vector3)offset;

            // Vytvoření malého orka
            GameObject newOrc = Instantiate(smallOrcPrefab, spawnPos, Quaternion.identity);

            // Přenesení pathIndexu
            EnemyMovement em = newOrc.GetComponent<EnemyMovement>();
            if (em != null)
            {
                em.SetPathIndex(cartPathIndex);
            }
        }
    }
}
