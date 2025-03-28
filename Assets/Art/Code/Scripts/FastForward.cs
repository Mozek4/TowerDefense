using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastForward : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button fastForwardButton;

    private bool isFastForward = false;

    void Start()
    {
        fastForwardButton.onClick.AddListener(fastForward);
    }
    private void fastForward() {
        if (Time.timeScale == 1 && isFastForward == false) {
            Time.timeScale = 1.5f;
            Debug.Log(Time.timeScale);
        }
        if (Time.timeScale == 1.5f && isFastForward == true) {
            Time.timeScale = 1;
            Debug.Log(Time.timeScale);
        }
        isFastForward = !isFastForward;
    }
}
