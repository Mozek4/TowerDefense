using UnityEngine;
using TMPro; // pro Dropdown TMP

public class MusicPlayer : MonoBehaviour
{
    [Header("Seznam hudby")]
    public AudioClip[] tracks;
    public TMP_Dropdown dropdown; // přiřadíš dropdown z Canvasu

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetupDropdown();
    }

    void SetupDropdown()
    {
        if (dropdown == null) return;

        dropdown.ClearOptions();

        // Naplníme názvy skladeb podle názvů AudioClipů
        var options = new System.Collections.Generic.List<string>();
        foreach (AudioClip clip in tracks)
        {
            options.Add(clip.name);
        }

        dropdown.AddOptions(options);

        // Přidáme listener na změnu
        dropdown.onValueChanged.AddListener(delegate { PlayTrack(dropdown.value); });

        // Automaticky pustí první skladbu
        if (tracks.Length > 0)
            PlayTrack(0);
    }

    public void PlayTrack(int index)
    {
        if (index < 0 || index >= tracks.Length) return;

        audioSource.clip = tracks[index];
        audioSource.Play();
    }
}
