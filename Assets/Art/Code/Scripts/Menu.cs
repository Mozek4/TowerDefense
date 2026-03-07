using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; // Nutné přidat pro práci s EventSystemem

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu() {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);

        // Toto vyřeší tvůj problém:
        // Zruší výběr (focus) jakéhokoliv UI prvku, na který jsi právě klikl
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void OnGUI() {
        currencyUI.text = LevelManager.main.gold.ToString();
    }

    public void SetSelected() {
        // Sem můžeš v budoucnu přidat logiku pro zvýraznění konkrétního tlačítka,
        // ale pro tvůj aktuální problém stačí SetSelectedGameObject(null) výše.
    }
}