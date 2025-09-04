using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUI2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI WaveCounterUI;
    private void OnGUI() {
        if (EnemySpawnerB.Instance != null) {
            WaveCounterUI.text = "Wave " + EnemySpawnerB.Instance.currentWave.ToString();
        }
    }
}
