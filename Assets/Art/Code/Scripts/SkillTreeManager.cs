using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Přidáno pro práci s Listem
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    [Header("Tlačítka Skillů (Přiřaď v Inspectoru)")]
    [SerializeField] private GameObject btnTowerDamage;
    [SerializeField] private GameObject btnTowerDamage2;
    [SerializeField] private GameObject btnTowerRange;
    [SerializeField] private GameObject btnTowerRange2;
    [SerializeField] private GameObject btnTowerAttackSpeed;
    [SerializeField] private GameObject btnTowerAttackSpeed2;

    private void Start()
    {
        // Spustíme kontrolu hned při načtení Skill Tree
        HideUnlockedSkills();
    }

    private void HideUnlockedSkills()
    {
        // Pokud PlayerData neexistují, přeskočíme
        if (PlayerData.instance == null) return;

        List<string> unlocked = PlayerData.instance.unlockedSkills;

        // Pokud seznam obsahuje daný skill, schováme jeho tlačítko
        if (unlocked.Contains("TowerDamage") && btnTowerDamage != null) btnTowerDamage.SetActive(false);
        if (unlocked.Contains("TowerDamage2") && btnTowerDamage2 != null) btnTowerDamage2.SetActive(false);
        if (unlocked.Contains("TowerRange") && btnTowerRange != null) btnTowerRange.SetActive(false);
        if (unlocked.Contains("TowerRange2") && btnTowerRange2 != null) btnTowerRange2.SetActive(false);
        if (unlocked.Contains("TowerAttackSpeed") && btnTowerAttackSpeed != null) btnTowerAttackSpeed.SetActive(false);
        if (unlocked.Contains("TowerAttackSpeed2") && btnTowerAttackSpeed2 != null) btnTowerAttackSpeed2.SetActive(false);
    }

    IEnumerator RemoveButtonEffect(GameObject button)
        {
            // Vypneme klikání, aby hráč nemohl kliknout během dvousekundové animace znovu
            Button btnComponent = button.GetComponent<Button>();
            if (btnComponent != null) btnComponent.interactable = false;

            Vector3 startScale = button.transform.localScale;
            Vector3 peakScale = startScale * 1.2f; // Zvětšení na 120 %

            // --- FÁZE 1: Tlačítko se pouze zvětšuje (žádné mizení) ---
            float expandDuration = 1.4f; // Tvůj čas pro zvětšení (2 sekundy)
            float time = 0;

            while (time < expandDuration)
            {
                time += Time.deltaTime;
                button.transform.localScale = Vector3.Lerp(startScale, peakScale, time / expandDuration);
                yield return null;
            }

            // Pojistka přesné velikosti před další fází
            button.transform.localScale = peakScale;

            // --- FÁZE 2: Tlačítko se rychle zmenší do nuly ---
            float shrinkDuration = 0.1f; // Velmi rychlé "vcucnutí"
            time = 0;

            while (time < shrinkDuration)
            {
                time += Time.deltaTime;
                button.transform.localScale = Vector3.Lerp(peakScale, Vector3.zero, time / shrinkDuration);
                yield return null;
            }

            // Pojistka, že je velikost na konci přesně na nule
            button.transform.localScale = Vector3.zero;

            // --- FÁZE 3: Zmizení (tlačítko se fyzicky deaktivuje) ---
            button.SetActive(false); 
        }

    public void UpgradeTowerDamage(GameObject button)
    {
        PlayerStats.instance.towerDamageMultiplier += 0.2f;
        PlayerData.instance.SaveSkill("TowerDamage");
        StartCoroutine(RemoveButtonEffect(button));
    }

    public void UpgradeTowerDamage2(GameObject button)
    {
        PlayerStats.instance.towerDamageMultiplier += 0.3f;
        PlayerData.instance.SaveSkill("TowerDamage2");
        StartCoroutine(RemoveButtonEffect(button));
    }

    public void UpgradeTowerRange(GameObject button)
    {
        PlayerStats.instance.towerRangeMultiplier += 0.1f;
        PlayerData.instance.SaveSkill("TowerRange");
        StartCoroutine(RemoveButtonEffect(button));
    }

    public void UpgradeTowerRange2(GameObject button)
    {
        PlayerStats.instance.towerRangeMultiplier += 0.15f;
        PlayerData.instance.SaveSkill("TowerRange2");
        StartCoroutine(RemoveButtonEffect(button));
    }

    public void UpgradeTowerAttackSpeed(GameObject button)
    {
        PlayerStats.instance.towerAttackSpeedMultiplier += 0.1f;
        PlayerData.instance.SaveSkill("TowerAttackSpeed");
        StartCoroutine(RemoveButtonEffect(button));
    }

    public void UpgradeTowerAttackSpeed2(GameObject button)
    {
        PlayerStats.instance.towerAttackSpeedMultiplier += 0.15f;
        PlayerData.instance.SaveSkill("TowerAttackSpeed2");
        StartCoroutine(RemoveButtonEffect(button));
    }
}