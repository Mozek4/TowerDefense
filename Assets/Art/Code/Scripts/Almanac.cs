using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenAlmanac : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject EnemiesAlmanac;
    [SerializeField] private GameObject TowersAlmanac;
    [SerializeField] private Button OpenAlmanacButton;
    [SerializeField] private Button CloseAlmanacButton;
    [SerializeField] private Button CloseAlmanacButton2;
    [SerializeField] private Button GoToTowersButton;
    [SerializeField] private Button GoToMonsterButton;

    private void Start()
    {
        GoToTowersButton.onClick.AddListener(GoToTowers);
        GoToMonsterButton.onClick.AddListener(GoToMonsters);
        OpenAlmanacButton.onClick.AddListener(OpenEnemiesAlmanac);
        CloseAlmanacButton.onClick.AddListener(CloseEnemiesAlmanac);
        CloseAlmanacButton2.onClick.AddListener(CloseEnemiesAlmanac);
        EnemiesAlmanac.SetActive(false);
        TowersAlmanac.SetActive(false);
    }

    private void OpenEnemiesAlmanac() {
        EnemiesAlmanac.SetActive(true);
    }
    
    private void CloseEnemiesAlmanac() {
        EnemiesAlmanac.SetActive(false);
        TowersAlmanac.SetActive(false);
    }

    private void GoToTowers() {
        EnemiesAlmanac.SetActive(false);
        TowersAlmanac.SetActive(true);
    }

    private void GoToMonsters() {
        EnemiesAlmanac.SetActive(true);
        TowersAlmanac.SetActive(false);
    }
}
