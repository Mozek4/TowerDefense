using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class VillageButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] path1;
    [SerializeField] private GameObject Path1;
    [SerializeField] private GameObject[] path2;
    [SerializeField] private GameObject Path2;
    [SerializeField] private GameObject[] path3;
    [SerializeField] private GameObject Path3;
    [SerializeField] private GameObject[] path4;
    [SerializeField] private GameObject Path4;
    [SerializeField] private GameObject house1;
    [SerializeField] private GameObject house2;
    [SerializeField] private GameObject house3;
    [SerializeField] private GameObject house4;
    [SerializeField] private GameObject house5;
    [SerializeField] private GameObject house6;
    [SerializeField] private GameObject house7;
    [SerializeField] private GameObject house8;
    [SerializeField] private GameObject house9;
    [SerializeField] private GameObject house10;
    [SerializeField] private GameObject house11;
    [SerializeField] private GameObject house12;
    [SerializeField] private GameObject Shop;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerData.instance.SaveVillage();
            PlayerData.instance.SaveDiamonds();
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
        PlayerData.instance.SaveVillage();
    }

    void Start()
    {
        Shop.SetActive(false);
        foreach (GameObject obj in path1)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in path2)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in path3)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in path4)
        {
            obj.SetActive(false);
        }
        house1.SetActive(false);
        house2.SetActive(false);
        house3.SetActive(false);
        house4.SetActive(false);
        house5.SetActive(false);
        house6.SetActive(false);
        house7.SetActive(false);
        house8.SetActive(false);
        house9.SetActive(false);
        house10.SetActive(false);
        house11.SetActive(false);
        house12.SetActive(false);
        Path1.SetActive(false);
        Path2.SetActive(false);
        Path3.SetActive(false);
        Path4.SetActive(false);
        StartCoroutine(LoadVillageDelayed());
    }

    public void OpenShop()
    {
        if (Shop.activeSelf)
        {
            Shop.SetActive(false);
        }
        else
        {
            Shop.SetActive(true);
        }
    }

    IEnumerator LoadVillageDelayed()
    {
        yield return new WaitForEndOfFrame();
        PlayerData.instance.LoadVillage();
    }
    public void FirstHouseButton()
    {
        if (PlayerData.instance.diamonds > 100 && !house1.activeSelf)
        {
            house1.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 100;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void SecondHouseButton()
    {
        if (PlayerData.instance.diamonds > 200 && !house2.activeSelf)
        {
            house2.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 200;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void ThirdHouseButton()
    {
        if (PlayerData.instance.diamonds > 300 && !house3.activeSelf)
        {
            house3.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 300;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void FourthHouseButton()
    {
        if (PlayerData.instance.diamonds > 600 && !house4.activeSelf)
        {
            house4.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 600;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void FifthHouseButton()
    {
        if (PlayerData.instance.diamonds > 1000 && !house5.activeSelf)
        {
            house5.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 1000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void SixthHouseButton()
    {
        if (PlayerData.instance.diamonds > 2000 && !house6.activeSelf)
        {
            house6.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 2000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void SeventhHouseButton()
    {
        if (PlayerData.instance.diamonds > 2000 && !house7.activeSelf)
        {
            house7.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 2000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void EighthHouseButton()
    {
        if (PlayerData.instance.diamonds > 2000 && !house8.activeSelf)
        {
            house8.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 2000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void NinthHouseButton()
    {
        if (PlayerData.instance.diamonds > 2000 && !house9.activeSelf)
        {
            house9.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 2000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void TenthHouseButton()
    {
        if (PlayerData.instance.diamonds > 2000 && !house10.activeSelf)
        {
            house10.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 2000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void EleventhHouseButton()
    {
        if (PlayerData.instance.diamonds > 2000 && !house11.activeSelf)
        {
            house11.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 2000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void TwelfthHouseButton()
    {
        if (PlayerData.instance.diamonds > 2000 && !house12.activeSelf)
        {
            house12.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 2000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }

    public void FirstPathButton()
    {
        if (PlayerData.instance.diamonds > 1000 && !path1[0].activeSelf)
        {
            foreach (GameObject obj in path1)
            {
                obj.SetActive(true);
            }
            Path1.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 1000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void SecondPathButton()
    {
        if (PlayerData.instance.diamonds > 3000 && !path2[0].activeSelf)
        {
            foreach (GameObject obj in path2)
            {
                obj.SetActive(true);
            }
            Path2.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 3000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void ThirdPathButton()
    {
        if (PlayerData.instance.diamonds > 5000 && !path3[0].activeSelf)
        {
            foreach (GameObject obj in path3)
            {
                obj.SetActive(true);
            }
            Path3.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 5000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
    public void FourthPathButton()
    {
        if (PlayerData.instance.diamonds > 8000 && !path4[0].activeSelf)
        {
            foreach (GameObject obj in path4)
            {
                obj.SetActive(true);
            }
            Path4.SetActive(true);
            PlayerData.instance.diamonds = PlayerData.instance.diamonds - 8000;
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }
    }
}
