using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BpsUpgradePriceCrossbowman : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeBpsPrice;
    [SerializeField] Crossbowman crossbowman;
    private void OnGUI() {
        if (upgradeBpsPrice != null && crossbowman != null) {
            upgradeBpsPrice.text = "Upgrade AS, Price: "+ crossbowman.CalculateBpsCost();
        }
    }
}
