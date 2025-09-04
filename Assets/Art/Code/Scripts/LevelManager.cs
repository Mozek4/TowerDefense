using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;
    public Transform startPoint2;
    public Transform[] path2;
    public int gold;
    public int towerCost;
    public static int playerHealth = 100;
    public int score;
    public static void playerHealthReduce(int amount) {
        playerHealth -= amount;
    }
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        gold = 37500;
        score = 0;
        playerHealth = 100;
    }

    public void IncreaseCurrency(int amount) {
        gold += amount;
    }

    public bool SpendCurrency(int amount) {
        if (amount <= gold) {
            gold -= amount;
            towerCost = amount;
            return true;
        } else {
            Debug.Log("Not Enough");
            return false;
        }
    }
}
