using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI WaveCounterUI;
    private void OnGUI() {
        if (EnemySpawner.Instance != null) {
            WaveCounterUI.text = "Wave " + EnemySpawner.Instance.currentWave.ToString();
        }
    }
}
