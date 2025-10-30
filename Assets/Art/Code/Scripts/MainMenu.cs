using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject firstLevel;
    [SerializeField] private GameObject secondLevel;
    [SerializeField] private GameObject thirdLevel;


    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && map.activeSelf)
        {
            map.SetActive(false);
        }
    }

    void Start()
    {
        map.SetActive(false);
        firstLevel.SetActive(false);
        secondLevel.SetActive(false);
        thirdLevel.SetActive(false);
    }
    public void OnFirstMapButtonClicked()
    {
        firstLevel.SetActive(true);
    }

    public void OnFirst2MapButtonClicked()
    {
        SceneManager.LoadScene("OriginalMap");
    }
    public void OnSecondMapButtonClicked()
    {
        secondLevel.SetActive(true);
    }
    public void OnSecond2MapButtonClicked()
    {
        SceneManager.LoadScene("SnowMap");
    }
    public void OnThirdMapButtonClicked()
    {
        thirdLevel.SetActive(true);
    }
    public void OnThirdMap2ButtonClicked()
    {
        SceneManager.LoadScene("MountainMap");
    }

    public void OnStartButtonClicked()
    {
        map.SetActive(true);
    }
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void OnVillageButtonClicked()
    {
        SceneManager.LoadScene("Village");
    }
}

