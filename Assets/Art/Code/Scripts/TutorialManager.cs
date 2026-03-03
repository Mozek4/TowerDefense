using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject tutorialPanel; 
    public Image displayImage;

    [Header("Slides")]
    public Sprite[] slides;

    [Header("Buttons")]
    public Button nextButton;
    public Button prevButton;
    public Button openButton;

    private int currentIndex = 0;

    void Start()
    {
        nextButton.onClick.AddListener(NextSlide);
        prevButton.onClick.AddListener(PrevSlide);
        openButton.onClick.AddListener(ToggleTutorial);

        tutorialPanel.SetActive(false);
    }

    void ShowSlide(int index)
    {
        if (slides.Length == 0) return;

        index = Mathf.Clamp(index, 0, slides.Length - 1);

        displayImage.sprite = slides[index];

        UpdateButtons();
    }

    void NextSlide()
    {
        if (currentIndex < slides.Length - 1)
        {
            currentIndex++;
            ShowSlide(currentIndex);
        }
    }

    void PrevSlide()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowSlide(currentIndex);
        }
    }

    void UpdateButtons()
    {
        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < slides.Length - 1;
    }

    void ToggleTutorial()
    {
        bool isActive = tutorialPanel.activeSelf;

        tutorialPanel.SetActive(!isActive);

        if (!isActive)
        {
            currentIndex = 0;
            ShowSlide(currentIndex);
        }
    }
}