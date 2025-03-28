using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartButton() {
        SceneManager.LoadScene("Main");
        LevelManager.playerHealth = 100;
        Time.timeScale =1;
    }
    public void ExitToMenu() {
        LevelManager.playerHealth = 100;
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
