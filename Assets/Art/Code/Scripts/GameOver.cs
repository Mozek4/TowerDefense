using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] string MapName;
    public void RestartButton()
    {
        SceneManager.LoadScene(MapName);
        LevelManager.playerHealth = 100;
        Time.timeScale = 1;
    }
    public void ExitToMenu()
    {
        int diamondsToAdd = 0;
        if (LevelManager.main != null)
        {
            diamondsToAdd = LevelManager.main.score / 20;
        }

        if (PlayerData.instance != null)
        {
            PlayerData.instance.AddDiamonds(diamondsToAdd);
        }

        LevelManager.playerHealth = 100;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
