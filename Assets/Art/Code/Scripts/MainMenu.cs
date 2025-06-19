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

    void Start()
    {
        map.SetActive(false);
    }
    public void OnFirstMapButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnSecondMapButtonClicked()
    {
        SceneManager.LoadScene("SnowMap");
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

