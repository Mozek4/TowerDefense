using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BpsUpgradeShow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeBpsText;
    [SerializeField] Turret turret;

    private void OnGUI()
    {
        if (upgradeBpsText != null && turret != null)
        {
            // Aktuální BPS
            float currentBps = turret.CalculateBPS();

            // Budoucí BPS po upgradu (simulace zvýšení levelu o 1)
            float nextBps = turret.ApsBaseTimes(Mathf.Pow(turret.BpsLevel + 1, 0.4f));

            // Zaokrouhlení na 2 desetinná místa
            currentBps = (float)Math.Round(currentBps, 2);
            nextBps = (float)Math.Round(nextBps, 2);

            upgradeBpsText.text =
                $"AS: {currentBps} -> {nextBps}\n";
        }
    }
}
