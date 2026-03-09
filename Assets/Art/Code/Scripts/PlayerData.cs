using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public int diamonds;

    public List<string> activeVillageObjects = new List<string>();
    public List<string> unlockedSkills = new List<string>();

    public void SaveSkill(string skillName)
    {
        if (!unlockedSkills.Contains(skillName))
        {
            unlockedSkills.Add(skillName);
            string data = string.Join(",", unlockedSkills);
            PlayerPrefs.SetString("UnlockedSkills", data);
            PlayerPrefs.Save();
        }
    }

    public void LoadSkills()
    {
        string data = PlayerPrefs.GetString("UnlockedSkills", "");
        if (!string.IsNullOrEmpty(data))
        {
            string[] ids = data.Split(',');
            unlockedSkills = new List<string>(ids);
        }

        foreach (string skill in unlockedSkills)
        {
            if (skill == "TowerDamage") PlayerStats.instance.towerDamageMultiplier += 0.2f;
            else if (skill == "TowerDamage2") PlayerStats.instance.towerDamageMultiplier += 0.3f; // CHYBĚLO
            
            else if (skill == "TowerRange") PlayerStats.instance.towerRangeMultiplier += 0.1f;
            else if (skill == "TowerRange2") PlayerStats.instance.towerRangeMultiplier += 0.15f; // CHYBĚLO
            
            else if (skill == "TowerAttackSpeed") PlayerStats.instance.towerAttackSpeedMultiplier += 0.1f; // CHYBĚLO
            else if (skill == "TowerAttackSpeed2") PlayerStats.instance.towerAttackSpeedMultiplier += 0.15f; // CHYBĚLO
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadDiamonds();
            LoadSkills();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
public void SaveVillage()
{
    List<string> activeIDs = new List<string>();
    // Získá všechny VillageObject, i ty, které jsou neaktivní
    foreach (VillageObject vo in FindObjectsOfType<VillageObject>(true))
    {
        if (vo.gameObject.activeSelf)
        {
            Debug.Log("Ukládám aktivní objekt: " + vo.objectID);
            activeIDs.Add(vo.objectID);
        }
    }
    string data = string.Join(",", activeIDs);
    PlayerPrefs.SetString("VillageObjects", data);
    PlayerPrefs.Save();
    Debug.Log("Uložená data: " + data);
}

public void LoadVillage()
{
    string data = PlayerPrefs.GetString("VillageObjects", "");
    Debug.Log("Načtena uložená data: " + data);

    if (!string.IsNullOrEmpty(data))
    {
        string[] ids = data.Split(',');
        activeVillageObjects = new List<string>(ids);
    }

    foreach (VillageObject vo in FindObjectsOfType<VillageObject>(true))
    {
        bool aktivovat = activeVillageObjects.Contains(vo.objectID);
        Debug.Log(vo.objectID + " bude " + (aktivovat ? "aktivní" : "neaktivní"));
        SetActiveRecursively(vo.gameObject, aktivovat);
        Debug.Log("Objekt " + vo.objectID + " má po aktivaci stav: " + vo.gameObject.activeSelf);
    }
}

// Pomocná metoda pro rekurzivní aktivaci/deaktivaci všech potomků
private void SetActiveRecursively(GameObject obj, bool active)
{
    obj.SetActive(active);
    foreach (Transform child in obj.transform)
    {
        SetActiveRecursively(child.gameObject, active);
    }
}


    public void SaveDiamonds()
    {
        PlayerPrefs.SetInt("Diamonds", diamonds);
        PlayerPrefs.Save();
    }

    public void LoadDiamonds()
    {
        diamonds = PlayerPrefs.GetInt("Diamonds", 0);
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        SaveDiamonds();
    }
}
