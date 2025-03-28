using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RangeUpgradePriceMage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeRangePrice;
    [SerializeField] MageTower mageTower;

    private void OnGUI() {
        if (upgradeRangePrice != null && mageTower != null) {
            upgradeRangePrice.text = "Upgrade Range, Price: "+ mageTower.CalculateRangeCost();
        }
    }
}
