using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RangeUpgradePriceCrossbowman : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeRangePrice;
    [SerializeField] Crossbowman crossbowman;

    private void OnGUI() {
        if (upgradeRangePrice != null && crossbowman != null) {
            upgradeRangePrice.text = "Upgrade Range, Price: "+ crossbowman.CalculateRangeCost();
        }
    }
}
