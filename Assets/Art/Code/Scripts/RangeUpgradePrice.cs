using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangeUpgradePrice : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeRangePrice;
    [SerializeField] Turret turret;

    private void OnGUI() {
        if (upgradeRangePrice != null && turret != null) {
            upgradeRangePrice.text = "Price: "+ turret.CalculateRangeCost();
        }
    }
}
