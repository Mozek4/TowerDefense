using UnityEngine;
using TMPro;

public class UpgradePrice : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Turret turret;

    [Header("Settings")]
    [SerializeField] private UpgradeType upgradeType;

    private void OnGUI()
    {
        if (priceText == null || turret == null) return;

        int price = GetPrice();
        priceText.text = "Cost: " + price;
    }

    private int GetPrice()
    {
        switch (upgradeType)
        {
            case UpgradeType.Range:
                return turret.CalculateRangeCost();

            case UpgradeType.Bps:
                return turret.CalculateBpsCost();

            default:
                return 0;
        }
    }
}


