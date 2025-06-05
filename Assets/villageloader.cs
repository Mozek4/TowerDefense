using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villageloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerData.instance.LoadVillage();
    }
}
