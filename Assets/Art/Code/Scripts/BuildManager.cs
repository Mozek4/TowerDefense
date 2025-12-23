using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] public Tower[] towers;

    [Header("UI Buttons")]
    [SerializeField] public RectTransform[] towerButtons;

    public int selectedTower = 0;
    private int lastSelected = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SetSelectedTower(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SetSelectedTower(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SetSelectedTower(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SetSelectedTower(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            SetSelectedTower(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            SetSelectedTower(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            SetSelectedTower(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            SetSelectedTower(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            SetSelectedTower(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            SetSelectedTower(9);
        }
    }

    private void Awake() {
        main = this;
    }
    
    public Tower GetSelectedTower() {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower) {

        // ZMENŠENÍ PŘEDCHOZÍHO TLAČÍTKA
        if (lastSelected != -1 && lastSelected < towerButtons.Length) {
            towerButtons[lastSelected].localScale = Vector3.one;
        }

        selectedTower = _selectedTower;

        // ZVĚTŠENÍ NOVÉHO TLAČÍTKA
        if (_selectedTower < towerButtons.Length) {
            towerButtons[_selectedTower].localScale = new Vector3(1.05f, 1.05f, 1f);
        }

        lastSelected = _selectedTower;
    }
}

