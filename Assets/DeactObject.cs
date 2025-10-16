using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeactObject : MonoBehaviour
{
    public GameObject targetButton; // to tlačítko, co se má vypínat

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // levé tlačítko myši
        {
            GameObject clicked = EventSystem.current.currentSelectedGameObject; 

            // Pokud bylo kliknuto a není to naše tlačítko
            if (clicked == null || clicked != targetButton)
            {
                targetButton.SetActive(false);
            }
        }
    }
}
