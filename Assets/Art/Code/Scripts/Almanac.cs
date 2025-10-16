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
    [SerializeField] private GameObject Enemy1;
    [SerializeField] private GameObject Enemy2;
    [SerializeField] private GameObject Enemy3;
    [SerializeField] private GameObject Enemy4;
    [SerializeField] private GameObject Enemy5;
    [SerializeField] private GameObject Enemy6;
    [SerializeField] private GameObject Enemy7;
    [SerializeField] private GameObject Enemy8;
    [SerializeField] private GameObject Enemy9;
    [SerializeField] private GameObject Enemy10;


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

    public void ActivateEnemy1()
    {
        Enemy1.SetActive(true);
    }
    public void ActivateEnemy2()
    {
        Enemy2.SetActive(true);
    }
    public void ActivateEnemy3()
    {
        Enemy3.SetActive(true);
    }
    public void ActivateEnemy4()
    {
        Enemy4.SetActive(true);
    }
    public void ActivateEnemy5()
    {
        Enemy5.SetActive(true);
    }
    public void ActivateEnemy6()
    {
        Enemy6.SetActive(true);
    }
    public void ActivateEnemy7()
    {
        Enemy7.SetActive(true);
    }
    public void ActivateEnemy8()
    {
        Enemy8.SetActive(true);
    }
    public void ActivateEnemy9()
    {
        Enemy9.SetActive(true);
    }
    public void ActivateEnemy10()
    {
        Enemy10.SetActive(true);
    }
    
}
