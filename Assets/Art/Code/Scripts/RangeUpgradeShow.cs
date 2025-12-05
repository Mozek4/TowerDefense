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
            // Aktuální range
            float currentRange = turret.CalculateRange();

            // Budoucí range (simulace, jako by měl o 1 level více)
            float nextRange = turret.RangeBaseTimes(Mathf.Pow(turret.RangeLevel + 1, 0.15f));

            // Zaokrouhlení
            currentRange = (float)Math.Round(currentRange, 2);
            nextRange = (float)Math.Round(nextRange, 2);

            upgradeRangeText.text =
                $"Range: {currentRange} -> {nextRange}\n";
        }
    }
}
