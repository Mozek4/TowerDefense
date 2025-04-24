using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardianTreeButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button GuardianButton;
    [SerializeField] private Animator isTreeGrown;

    public GameObject GuardianTree;
    private int GuardianTreeCost = 3000;

    private void Start() {
        GuardianButton.onClick.AddListener(BuyGuardianTree);
        if (GuardianTree == null) {
            GuardianTree = GameObject.Find("Guardian Tree");
        }
        GuardianTree.SetActive(false);
    }

    private void BuyGuardianTree() {
        if (LevelManager.main.gold > GuardianTreeCost) {
            LevelManager.main.gold -= GuardianTreeCost;
            if (isTreeGrown != null) {
                GuardianButton.gameObject.SetActive(false);
                GuardianTree.SetActive(true);
                //GuardianButton.interactable = false;
            }
        }
    }
}
