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
            float currentBps = turret.CalculateBPS();

            float nextBps = turret.ApsBaseTimes(Mathf.Pow(turret.BpsLevel + 1, 0.4f));

            currentBps = (float)Math.Round(currentBps, 2);
            nextBps = (float)Math.Round(nextBps, 2);

            upgradeBpsText.text =
                $"AS: {currentBps} -> {nextBps}\n";
        }
    }
}
