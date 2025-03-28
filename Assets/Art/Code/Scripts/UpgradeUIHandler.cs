using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    //private bool mouseOver;
    private GameObject UpgradeCanvas;

    [SerializeField] LineRenderer rangeIndicator;

    public void OnPointerEnter(PointerEventData eventData) {
        //mouseOver = true;
        UIManager.main.SetHoveringState(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        //mouseOver = false;
        UIManager.main.SetHoveringState(false);
        UpgradeCanvas = GameObject.Find("UpgradeUI");
        if (UpgradeCanvas != null) {
            UpgradeCanvas.SetActive(false);
            rangeIndicator.enabled = false;
        }

    }
}

