using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
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

        Destroy(button);
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
