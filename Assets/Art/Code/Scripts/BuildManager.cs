using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] public Tower[] towers;

    public int selectedTower = 0;

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
