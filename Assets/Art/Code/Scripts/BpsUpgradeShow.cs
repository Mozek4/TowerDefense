using System;
using TMPro;
using UnityEngine;

public class BpsUpgradeShow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeBpsText;
    [SerializeField] Turret turret;

    // Změnil jsem OnGUI na Update, protože OnGUI se volá několikrát za snímek 
    // a je pro TextMeshPro zbytečně náročné.
    private void Update()
    {
        if (upgradeBpsText != null && turret != null)
        {
            // 1. Získáme globální násobitel z PlayerStats (pokud existuje)
            float globalMult = PlayerStats.instance != null ? PlayerStats.instance.towerAttackSpeedMultiplier : 1f;

            // 2. Výpočet aktuálního BPS (Základ * Level bonus * SkillTree bonus)
            float currentBps = turret.CalculateBPS() * globalMult;

            // 3. Výpočet budoucího BPS pro další level (Základ * Bonus pro level + 1 * SkillTree bonus)
            float nextBps = turret.ApsBaseTimes(Mathf.Pow(turret.BpsLevel + 1, 0.4f)) * globalMult;

            // Zaokrouhlení pro hezčí UI
            currentBps = (float)Math.Round(currentBps, 2);
            nextBps = (float)Math.Round(nextBps, 2);

            upgradeBpsText.text = $"AS: {currentBps} -> {nextBps}\n";
        }
    }
}