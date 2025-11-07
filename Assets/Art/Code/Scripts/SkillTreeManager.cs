using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public void UpgradeTowerDamage()
    {
        PlayerStats.instance.towerDamageMultiplier += 0.2f;
        PlayerData.instance.SaveSkill("TowerDamage");
        Debug.Log("Skill TowerDamage odemčen a uložen.");
    }

    public void UpgradeTowerDamage2()
    {
        PlayerStats.instance.towerDamageMultiplier += 0.3f;
        PlayerData.instance.SaveSkill("TowerDamage2");
        Debug.Log("Skill TowerDamage2 odemčen a uložen.");
    }

    public void UpgradeTowerRange()
    {
        PlayerStats.instance.towerRangeMultiplier += 0.1f;
        PlayerData.instance.SaveSkill("TowerRange");
        Debug.Log("Skill TowerRange odemčen a uložen.");
    }

    public void UpgradeTowerRange2()
    {
        PlayerStats.instance.towerRangeMultiplier += 0.15f;
        PlayerData.instance.SaveSkill("TowerRange2");
        Debug.Log("Skill TowerRange2 odemčen a uložen.");
    }

    public void UpgradeTowerAttackSpeed()
    {
        PlayerStats.instance.towerAttackSpeedMultiplier += 0.1f;
        PlayerData.instance.SaveSkill("TowerAttackSpeed");
        Debug.Log("Skill TowerAttackSpeed odemčen a uložen.");
    }

    public void UpgradeTowerAttackSpeed2()
    {
        PlayerStats.instance.towerAttackSpeedMultiplier += 0.15f;
        PlayerData.instance.SaveSkill("TowerAttackSpeed2");
        Debug.Log("Skill TowerAttackSpeed2 odemčen a uložen.");
    }
}
