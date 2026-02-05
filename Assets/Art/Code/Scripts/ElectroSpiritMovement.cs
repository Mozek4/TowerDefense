using UnityEngine;

public class ElectroSpiritMovement : EnemyMovement
{
    [Header("Electro Spirit Settings")]
    [SerializeField] private float waveAmplitude = 2f;
    [SerializeField] private float waveFrequency = 5f;
    [SerializeField] private float spiritSpeed = 2f;

    private Rigidbody2D myRb;

    private void Start()
    {
        // 1. Načteme RB
        myRb = GetComponent<Rigidbody2D>();

        // 2. OPRAVA PROBLÉMU:
        // Protože tento Start() "přepsal" Start() v rodiči (EnemyMovement),
        // rodič si nikdy nenastavil 'target'. Musíme to udělat ručně.
        // Zavoláme veřejnou metodu z rodiče, která nastaví index na 0 a najde target.
        SetPathIndex(0);
    }

    private void FixedUpdate()
    {
        // Získáme index
        int currentIndex = GetPathIndex();

        // Bezpečnostní kontrola
        if (LevelManager.main == null || LevelManager.main.path == null || currentIndex >= LevelManager.main.path.Length)
            return;

        Transform currentTarget = LevelManager.main.path[currentIndex];

        // Pokud by target byl stále null (např. chyba v LevelManageru), vyskočíme, aby to nespadlo
        if (currentTarget == null) return;

        Vector2 direction = (currentTarget.position - transform.position).normalized;
        float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

        Vector2 finalVelocity;

        // Logika osy (X vs Y)
        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            finalVelocity = new Vector2(direction.x * spiritSpeed + waveOffset, direction.y * spiritSpeed);
        }
        else
        {
            finalVelocity = new Vector2(direction.x * spiritSpeed, direction.y * spiritSpeed + waveOffset);
        }

        myRb.velocity = finalVelocity;
    }
}