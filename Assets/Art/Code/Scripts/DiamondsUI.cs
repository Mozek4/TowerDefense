using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiamondsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI DiamondCounterUI;
    private void OnGUI()
    {
        if (DiamondCounterUI != null && PlayerData.instance != null)
        {
            DiamondCounterUI.text = PlayerData.instance.diamonds.ToString();
        }
        else
        {
            DiamondCounterUI.text = 0.ToString();
        }
    }   
}
