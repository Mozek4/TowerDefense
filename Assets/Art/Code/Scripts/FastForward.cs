using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FastForward : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button fastForwardButton;
    [SerializeField] private GameObject arrow2;
    [SerializeField] private GameObject pause;

    private float previousScale;

    private bool isFastForward = false;

    void Start()
    {
        pause.SetActive(false);
        arrow2.SetActive(false);
        fastForwardButton.onClick.AddListener(fastForward);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fastForward();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause.activeSelf) 
            {
                pause.SetActive(false);
                Time.timeScale = previousScale;
            }
            else 
            {
                previousScale = Time.timeScale;
                pause.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    private void fastForward() {
        if (Time.timeScale == 1 && isFastForward == false) {
            Time.timeScale = 10f;
            arrow2.SetActive(true);
            Debug.Log(Time.timeScale);
        }
        if (Time.timeScale == 10 && isFastForward == true) {
            Time.timeScale = 1;
            arrow2.SetActive(false);
            Debug.Log(Time.timeScale);
        }
        isFastForward = !isFastForward;
    }
}
