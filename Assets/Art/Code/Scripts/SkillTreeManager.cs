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

    // --- Zbytek tvého kódu zůstává beze změny ---

    IEnumerator RemoveButtonEffect(GameObject button)
    {
        CanvasGroup cg = button.GetComponent<CanvasGroup>();

        if (cg == null)
            cg = button.AddComponent<CanvasGroup>();

        float duration = 0.4f;
        float time = 0;

        Vector3 startScale = button.transform.localScale;

        while (time < duration)
        {
            time += Time.deltaTime;

            cg.alpha = Mathf.Lerp(1f, 0f, time / duration);
            button.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, time / duration);

            yield return null;
        }

        button.SetActive(false); // ZMĚNA: Místo Destroy(button) použijeme jen deaktivaci
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