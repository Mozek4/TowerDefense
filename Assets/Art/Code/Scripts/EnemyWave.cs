using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyWave
{
    public float delayBeforeWave = 3f;
    public float enemiesPerSecond = 1f;
    public List<EnemySpawnEntry> enemies;
}