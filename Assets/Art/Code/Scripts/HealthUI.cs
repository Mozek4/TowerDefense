using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI HealthCounterUI;
    private void OnGUI() {
        if (HealthCounterUI != null) {
            HealthCounterUI.text = "Health " + LevelManager.playerHealth.ToString();
        }
    }   
}
