using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int towerCost;
    public static int playerHealth = 100;
    public static void playerHealthReduce(int amount) {
        playerHealth -= amount;
    }
    private void Awake() {
        main = this;
    }

    private void Start() {
        currency = 125000;
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if (amount <= currency) {
            currency -= amount;
            towerCost = amount;
            return true;
        } else {
            Debug.Log("Not Enough");
            return false;
        }
    }
}
