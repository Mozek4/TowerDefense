using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text descriptionText;

    [Header("Upgrade Values")]
    public float damageUpgradeValue;
    public float attackSpeedUpgradeValue;
    public float rangeUpgradeValue;

    [Header("Show In Tooltip")]
    public bool showDamage;
    public bool showAttackSpeed;
    public bool showRange;

    public void OnPointerEnter(PointerEventData eventData)
    {
        string text = "";

        if (showDamage)
        {
            int percent = Mathf.RoundToInt(damageUpgradeValue * 100);
            text += "Tower Damage +" + percent + "%\n";
        }

        if (showAttackSpeed)
        {
            int percent = Mathf.RoundToInt(attackSpeedUpgradeValue * 100);
            text += "Attack Speed +" + percent + "%\n";
        }

        if (showRange)
        {
            int percent = Mathf.RoundToInt(rangeUpgradeValue * 100);
            text += "Range +" + percent + "%\n";
        }

        descriptionText.text = text;
        descriptionText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.gameObject.SetActive(false);
    }
}