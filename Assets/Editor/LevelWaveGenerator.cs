using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelWaveGenerator : EditorWindow
{
    private LevelConfig levelConfig;

    private GameObject enemyPrefab;

    public List<GameObject> enemyPrefabs = new List<GameObject>();

    private int numberOfWaves = 15;
    private int baseEnemyCount = 5;

    [MenuItem("Tools/Generate Level Waves")]
    public static void ShowWindow()
    {
        GetWindow<LevelWaveGenerator>("Wave Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Wave Generator", EditorStyles.boldLabel);

        levelConfig = (LevelConfig)EditorGUILayout.ObjectField(
            "Level Config",
            levelConfig,
            typeof(LevelConfig),
            false);

        enemyPrefab = (GameObject)EditorGUILayout.ObjectField(
            "Enemy Prefab",
            enemyPrefab,
            typeof(GameObject),
            false);

        numberOfWaves = EditorGUILayout.IntField("Number Of Waves", numberOfWaves);
        baseEnemyCount = EditorGUILayout.IntField("Base Enemy Count", baseEnemyCount);

        if (GUILayout.Button("Generate Waves"))
        {
            Generate();
        }
    }

    void Generate()
    {
        if (levelConfig == null || enemyPrefabs.Count == 0)
        {
            Debug.LogError("Missing LevelConfig or enemy prefabs");
            return;
        }

        levelConfig.waves = new List<EnemyWave>();

        for (int i = 0; i < numberOfWaves; i++)
        {
            EnemyWave wave = new EnemyWave();

            wave.delayBeforeWave = 5f;
            wave.enemiesPerSecond = 0.5f + (i * 0.1f);

            wave.enemies = new List<EnemySpawnEntry>();

            int enemyTypesAvailable = Mathf.Clamp(i / 3 + 1, 1, enemyPrefabs.Count);

            int totalEnemies = baseEnemyCount + (i * 3);

            for (int j = 0; j < enemyTypesAvailable; j++)
            {
                EnemySpawnEntry entry = new EnemySpawnEntry();

                entry.enemyPrefab = enemyPrefabs[j];

                int portion = Mathf.RoundToInt(totalEnemies / (float)enemyTypesAvailable);

                entry.count = portion;

                wave.enemies.Add(entry);
            }

            levelConfig.waves.Add(wave);
        }

        EditorUtility.SetDirty(levelConfig);
        AssetDatabase.SaveAssets();

        Debug.Log("Complex waves generated!");
    }
}