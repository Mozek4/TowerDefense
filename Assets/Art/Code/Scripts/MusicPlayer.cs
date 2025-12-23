using UnityEngine;
using TMPro;
using UnityEngine.UI; // Slider

public class MusicPlayer : MonoBehaviour
{
    [Header("Seznam hudby")]
    public AudioClip[] tracks;
    public TMP_Dropdown dropdown;

    [Header("Ovládání hlasitosti")]
    [SerializeField] private Slider volumeSlider;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        LoadVolume();        // ⬅️ NEJDŘÍV data
        SetupDropdown();
        SetupVolumeSlider(); // ⬅️ pak UI
    }

    void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        audioSource.volume = savedVolume;

        if (volumeSlider != null)
            volumeSlider.value = savedVolume;
    }



    void SetupDropdown()
    {
        if (dropdown == null) return;

        dropdown.ClearOptions();

        var options = new System.Collections.Generic.List<string>();
        foreach (AudioClip clip in tracks)
        {
            options.Add(clip.name);
        }

        dropdown.AddOptions(options);
        dropdown.onValueChanged.AddListener(delegate { PlayTrack(dropdown.value); });

        if (tracks.Length > 0)
            PlayTrack(0);
    }

    void SetupVolumeSlider()
    {
        if (volumeSlider == null) return;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }


    public void PlayTrack(int index)
    {
        if (index < 0 || index >= tracks.Length) return;

        audioSource.clip = tracks[index];
        audioSource.Play();
    }
}
