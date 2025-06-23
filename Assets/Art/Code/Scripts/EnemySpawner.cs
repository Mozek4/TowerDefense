using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Random=UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {
    public static EnemySpawner Instance {get; private set;}

    [Header("Attributes")]
    [SerializeField] private List<GameObject> enemies; 
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficulty = 0.75f;
    [SerializeField] private float enemiesPerSecondLimit = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private bool GameIsOver = false;

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps;
    private bool isSpawning = false;

    public GameObject panel;

    private GameObject skeletonBoss;
    private GameObject vampireBoss;
    private GameObject skeleton;
    private GameObject goblin;
    private GameObject wolf;
    private GameObject tank;
    private GameObject electroSpirit;
    private GameObject spider;
    private GameObject miniRobot;
    private GameObject witch;
    private GameObject car;
    private GameObject roboticSnake;
    private GameObject greenTroll;
    private GameObject blueTroll;
    private GameObject redTroll;

    private void Awake() {
        enemies = new List<GameObject>();;
        skeletonBoss = Resources.Load<GameObject>("Enemies/SkeletonBoss");
        vampireBoss = Resources.Load<GameObject>("Enemies/VampireBoss");
        skeleton = Resources.Load<GameObject>("Enemies/Skeleton");
        goblin = Resources.Load<GameObject>("Enemies/Goblin");
        wolf = Resources.Load<GameObject>("Enemies/Wolf");
        tank = Resources.Load<GameObject>("Enemies/Tank");
        electroSpirit = Resources.Load<GameObject>("Enemies/ElectroSpirit");
        spider = Resources.Load<GameObject>("Enemies/Spider");
        miniRobot = Resources.Load<GameObject>("Enemies/MiniRobot");
        witch = Resources.Load<GameObject>("Enemies/Witch");
        car = Resources.Load<GameObject>("Enemies/Car");
        roboticSnake = Resources.Load<GameObject>("Enemies/RoboticSnake");
        greenTroll = Resources.Load<GameObject>("Enemies/GreenTroll");
        blueTroll = Resources.Load<GameObject>("Enemies/BlueTroll");
        redTroll = Resources.Load<GameObject>("Enemies/RedTroll");

        if (Instance == null) {
            Instance = this;
        }
            else {
            Destroy(gameObject);
            }
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start() {
        StartCoroutine(StartWave());
        if (panel == null) {
            panel = GameObject.Find("Game Over");
        }
        panel.SetActive(false);

    }   

    private void Update() {
        AddingEnemies();
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive <= 0 && enemiesLeftToSpawn == 0) {
            EndWave();
        }
        EndGame();
    }
    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private IEnumerator StartWave() {
        yield return new WaitForSeconds(timeBetweenWaves);

        BossSpawner();

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond() / 1.5f;
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }
    private void AddingEnemies() {
        if (currentWave == 1 && !enemies.Contains(skeleton)) {
            enemies.Add(skeleton);
        }
        if (currentWave == 5 && !enemies.Contains(goblin)) {
            enemies.Add(goblin);
        }
        if (currentWave == 10 && !enemies.Contains(spider)) {
            enemies.Add(spider);
        }
        if (currentWave == 15 && !enemies.Contains(wolf)) {
            enemies.Add(wolf);
            enemies.Remove(skeleton);
        }
        if (currentWave == 20 && !enemies.Contains(electroSpirit)) {
            enemies.Add(electroSpirit);
        }
        if (currentWave == 25 && !enemies.Contains(greenTroll)) {
            enemies.Add(greenTroll);
            enemies.Remove(goblin);
        }
        if (currentWave == 30 && !enemies.Contains(tank)) {
            enemies.Add(tank);
        }
        if (currentWave == 35 && !enemies.Contains(witch)) {
            enemies.Add(witch);
            enemies.Remove(spider);
        }
        if (currentWave == 40 && !enemies.Contains(roboticSnake)) {
            enemies.Add(roboticSnake);
            enemies.Remove(wolf);
        }
        if (currentWave == 45 && !enemies.Contains(blueTroll)) {
            enemies.Add(blueTroll);
        }
        if (currentWave == 50 && !enemies.Contains(miniRobot)) {
            enemies.Add(miniRobot);
        }
        if (currentWave == 55 && !enemies.Contains(redTroll)) {
            enemies.Add(redTroll);
        }
    }

    private void BossSpawner() {
        if (currentWave % 10 == 0 ) {
            Instantiate(skeletonBoss, LevelManager.main.startPoint.position, Quaternion.identity);
        }
        if (currentWave % 10 == 0 && currentWave > 19) {
            Instantiate(vampireBoss, LevelManager.main.startPoint.position, Quaternion.identity);
        }
        if (currentWave % 3 == 0 && currentWave > 11) {
            Instantiate(car, LevelManager.main.startPoint.position, Quaternion.identity);
        }
    }
    
    private void SpawnEnemy() {
        int index = Random.Range(0, enemies.Count);
        GameObject prefabToSpawn = enemies[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficulty));
    }

    private float EnemiesPerSecond() {
        return Math.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficulty),0f, enemiesPerSecondLimit);
    }

    private void EndGame(){
        if (LevelManager.playerHealth <= 0 && !GameIsOver)
        {
            GameIsOver = true;
            Time.timeScale = 0;
            panel.SetActive(true);
            PlayerData.instance.AddDiamonds(LevelManager.main.score / 20);
        }
    }
}


