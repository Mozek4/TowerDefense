using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] public Tower[] towers;

    public int selectedTower = 0;

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
    }

    private void Awake() {
        main = this;
    }
    
    public Tower GetSelectedTower() {
        return towers[selectedTower];
    }
    public void SetSelectedTower(int _selectedTower) {
        selectedTower = _selectedTower;
    }
}
