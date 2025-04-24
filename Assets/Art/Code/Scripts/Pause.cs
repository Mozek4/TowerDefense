using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject ffArrow;

    void Start()
    {
        pause.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause.activeSelf) {

                pause.SetActive(false);
                Time.timeScale = 1f;
                ffArrow.SetActive(false);
            }
            else {
                pause.SetActive(true);
                Time.timeScale = 0f;
            }

        }
    }
}
