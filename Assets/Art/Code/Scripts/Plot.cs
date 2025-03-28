using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObject;
    public Turret turret;
    public MageTower mage;
    public Crossbowman crossbowman;
    public IceTurret iceTurret;
    public BushTurret bushTurret;
    private Color startColor;    

    private void Start() {
        startColor = sr.color;
    }
    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        if (UIManager.main.IsHoveringUI()) {
            return;
        }
        
        if (towerObject != null) {
            if (towerObject.GetComponent<Turret>() != null) {
                towerObject.GetComponent<Turret>().OpenUpgradeUI();
            }
            else if (towerObject.GetComponent<MageTower>() != null) {
                towerObject.GetComponent<MageTower>().OpenUpgradeUI();
            }
            else if (towerObject.GetComponent<Crossbowman>() != null) {
                towerObject.GetComponent<Crossbowman>().OpenUpgradeUI();
            }
            else if (towerObject.GetComponent<IceTurret>() != null) {
                towerObject.GetComponent<IceTurret>().OpenUpgradeUI();
            }
            else if (towerObject.GetComponent<BushTurret>() != null) {
                towerObject.GetComponent<BushTurret>().OpenUpgradeUI();
            }
            return;
        }
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency) {
            Debug.Log("Cant afford");
            return;
        }
        LevelManager.main.SpendCurrency(towerToBuild.cost);

        towerObject = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);

    if (towerObject.GetComponent<Turret>() != null) {
        turret = towerObject.GetComponent<Turret>();
        }
    if (towerObject.GetComponent<MageTower>() != null) {
        mage = towerObject.GetComponent<MageTower>();
        }
    if (towerObject.GetComponent<Crossbowman>() != null) {
        crossbowman = towerObject.GetComponent<Crossbowman>();
        }
    }
}


