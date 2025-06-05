using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private GameObject VillageButton;
    [SerializeField] private GameObject DiamondCounter;

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

    private void OpenEnemiesAlmanac()
    {
        EnemiesAlmanac.SetActive(true);
        VillageButton.SetActive(false);
        DiamondCounter.SetActive(false);
    }

    private void CloseEnemiesAlmanac()
    {
        EnemiesAlmanac.SetActive(false);
        TowersAlmanac.SetActive(false);
        VillageButton.SetActive(true);
        DiamondCounter.SetActive(true);
    }

    private void GoToTowers()
    {
        EnemiesAlmanac.SetActive(false);
        TowersAlmanac.SetActive(true);
    }

    private void GoToMonsters()
    {
        EnemiesAlmanac.SetActive(true);
        TowersAlmanac.SetActive(false);
    }
}
