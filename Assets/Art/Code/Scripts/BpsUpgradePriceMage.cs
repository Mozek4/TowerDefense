using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BpsUpgradePriceMage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeBpsPrice;
    [SerializeField] MageTower mageTower;
    private void OnGUI() {
        if (upgradeBpsPrice != null && mageTower != null) {
            upgradeBpsPrice.text = "Upgrade AS, Price: "+ mageTower.CalculateBpsCost();
        }
    }
}
