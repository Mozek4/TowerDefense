using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenAlmanac : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject EnemiesAlmanac;
    [SerializeField] private Button OpenAlmanacButton;
    [SerializeField] private Button CloseAlmanacButton;

    private void Start()
    {
        OpenAlmanacButton.onClick.AddListener(OpenEnemiesAlmanac);
        CloseAlmanacButton.onClick.AddListener(CloseEnemiesAlmanac);
        EnemiesAlmanac.SetActive(false);
    }

    private void OpenEnemiesAlmanac() {
            EnemiesAlmanac.SetActive(true);
        }
    
    private void CloseEnemiesAlmanac() {
            EnemiesAlmanac.SetActive(false);
        }
}
