using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "TD/Level Config")]
public class LevelConfig : ScriptableObject
{
    public List<EnemyWave> waves;
}
