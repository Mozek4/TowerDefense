using UnityEngine;

public class DebugResetSave : MonoBehaviour
{
#if UNITY_EDITOR
    void OnGUI()
    {
        // Vykreslí jednoduché tlačítko v levém horním rohu obrazovky
        if (GUI.Button(new Rect(10, 10, 200, 30), "Reset Save (DEBUG)"))
        {
            ResetSave();
        }
    }

    void ResetSave()
    {
        // 1) Smažeme PlayerPrefs (včetně všeho)
        PlayerPrefs.DeleteAll();

        // 2) Pro jistotu smažeme i konkrétní klíče (pokud jsi používal jiné klíče)
        PlayerPrefs.DeleteKey("Diamonds");
        PlayerPrefs.DeleteKey("VillageObjects");
        PlayerPrefs.DeleteKey("UnlockedSkills");
        PlayerPrefs.Save();

        // 3) Vyčistíme runtime instance, pokud existují
        if (PlayerData.instance != null)
        {
            // reset v paměti
            PlayerData.instance.diamonds = 0;
            PlayerData.instance.activeVillageObjects.Clear();

            // pokud máš seznam odemčených skillů v PlayerData
            // (podle předchozí dohody by to mělo být unlockedSkills)
            var unlockedField = PlayerData.instance.GetType().GetField("unlockedSkills");
            if (unlockedField != null)
            {
                var list = unlockedField.GetValue(PlayerData.instance) as System.Collections.IList;
                if (list != null) list.Clear();
            }
            else
            {
                // pokud máš unlockedSkills jako public List<string>, můžeš přímo
                // PlayerData.instance.unlockedSkills.Clear();
            }

            // uložíme konzistentní stav (tvoje metody)
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }

        // 4) Reset gameplay multiplikátorů v PlayerStats (pokud existuje)
        if (PlayerStats.instance != null)
        {
            PlayerStats.instance.towerDamageMultiplier = 1f;
            PlayerStats.instance.towerRangeMultiplier = 1f;
            PlayerStats.instance.towerAttackSpeedMultiplier = 1f;
        }

        Debug.Log("Save data reset (PlayerPrefs cleared, runtime data cleared).");
    }
#endif
}
