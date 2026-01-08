using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [Header("Level Data")]
    [SerializeField] private LevelConfig levelConfig;

    [Header("Game Over")]
    [SerializeField] private GameObject panel;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private bool waveInProgress = false;


    private Queue<GameObject> spawnQueue = new Queue<GameObject>();

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;

    private float enemiesPerSecond;
    private float timeSinceLastSpawn;
    private bool isSpawning = false;
    private bool gameIsOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        if (panel == null)
            panel = GameObject.Find("Game Over");

        if (panel != null)
            panel.SetActive(false);

        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning)
            return;

        timeSinceLastSpawn += Time.deltaTime;

        if (spawnQueue.Count > 0 && timeSinceLastSpawn >= (1f / enemiesPerSecond))
        {
            SpawnNextEnemy();
            timeSinceLastSpawn = 0f;
        }

    if (waveInProgress && spawnQueue.Count == 0 && enemiesAlive <= 0)
    {
        EndWave();
    }
        EndGame();
    }

    private IEnumerator StartWave()
    {
        if (currentWaveIndex >= levelConfig.waves.Count)
        {
            LevelComplete();
            yield break;
        }

        EnemyWave wave = levelConfig.waves[currentWaveIndex];

        yield return new WaitForSeconds(wave.delayBeforeWave);

        PrepareSpawnQueue(wave);

        enemiesPerSecond = Mathf.Max(0.1f, wave.enemiesPerSecond);
        timeSinceLastSpawn = 0f;
        isSpawning = true;
        waveInProgress = true;
    }


    private void PrepareSpawnQueue(EnemyWave wave)
    {
        spawnQueue.Clear();

        foreach (EnemySpawnEntry entry in wave.enemies)
        {
            for (int i = 0; i < entry.count; i++)
            {
                spawnQueue.Enqueue(entry.enemyPrefab);
            }
        }
    }

    private void SpawnNextEnemy()
    {
        if (spawnQueue.Count == 0)
            return;

        GameObject prefab = spawnQueue.Dequeue();
        Instantiate(prefab, LevelManager.main.startPoint.position, Quaternion.identity);
        enemiesAlive++;
    }

    private void EndWave()
    {
        isSpawning = false;
        waveInProgress = false;
        currentWaveIndex++;
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    public int CurrentWaveNumber
    {
        get { return currentWaveIndex + 1; }
    }

    public int TotalWaves
    {
        get { return levelConfig != null ? levelConfig.waves.Count : 0; }
    }


    private void LevelComplete()
    {
        Debug.Log("LEVEL COMPLETE");
    }

    private void EndGame()
    {
        if (LevelManager.playerHealth <= 0 && !gameIsOver)
        {
            gameIsOver = true;
            Time.timeScale = 0f;

            if (panel != null)
                panel.SetActive(true);

            if (PlayerData.instance != null && LevelManager.main != null)
            {
                PlayerData.instance.AddDiamonds(LevelManager.main.score / 20);
            }
        }
    }

}
