using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnExitButtonClicked() 
    {
        Application.Quit();
    }
}

