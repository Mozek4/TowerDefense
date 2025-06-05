using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteData : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstRun"))
        {
            // První spuštění
            PlayerPrefs.DeleteAll();
            Debug.Log("První spuštění – smazána všechna data.");

            PlayerPrefs.SetInt("FirstRun", 1);
            PlayerPrefs.Save();
        }
    }
}
