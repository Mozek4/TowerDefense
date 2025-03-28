using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BpsUpgradePrice : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeBpsPrice;
    [SerializeField] Turret turret;
    private void OnGUI() {
        if (upgradeBpsPrice != null && turret != null) {
            upgradeBpsPrice.text = "Upgrade AS, Price: "+ turret.CalculateBpsCost();
        }
    }
}

