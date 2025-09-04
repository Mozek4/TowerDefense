using UnityEngine;
public class DebugResetSave : MonoBehaviour
{
#if UNITY_EDITOR
    void OnGUI()
    {
        // Vykreslí jednoduché tlačítko v levém horním rohu obrazovky
        if (GUI.Button(new Rect(10, 10, 150, 30), "Reset Save (DEBUG)"))
        {
            ResetSave();
        }
    }

    void ResetSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if (PlayerData.instance != null)
        {
            PlayerData.instance.diamonds = 0;
            PlayerData.instance.activeVillageObjects.Clear();
            PlayerData.instance.SaveDiamonds();
            PlayerData.instance.SaveVillage();
        }

        Debug.Log("Save data reset.");
    }
#endif
}