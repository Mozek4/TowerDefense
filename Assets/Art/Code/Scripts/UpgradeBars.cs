using System.Collections.Generic;
using UnityEngine;

public class UpgradeBars : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<GameObject> bars;   // tvoje čárky
    [SerializeField] Turret turret;           // věž, kde čteš level

    private void Update()
    {
        UpdateBars();
    }

    private void UpdateBars()
    {
        int level = turret.BpsLevel;   // nebo RangeLevel – podle toho, co potřebuješ

        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].SetActive(i < level);
        }
    }
}
