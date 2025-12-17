using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangeUpgradeShow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeRangeText;
    [SerializeField] Turret turret;

    private void OnGUI()
    {
        if (upgradeRangeText != null && turret != null)
        {
            float currentRange = turret.CalculateRange();

            float nextRange = turret.RangeBaseTimes(Mathf.Pow(turret.RangeLevel + 1, 0.15f));

            currentRange = (float)Math.Round(currentRange, 2);
            nextRange = (float)Math.Round(nextRange, 2);

            upgradeRangeText.text =
                $"RG: {currentRange} -> {nextRange}\n";
        }
    }
}
