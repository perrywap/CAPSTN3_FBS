using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuSimpleManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject optionsPanel;

    [Header("Volume Controls")]
    [SerializeField] private GameObject volumeControls;
    [SerializeField] private Slider volumeSlider;

    [Header("Buttons")]
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button resumegame;

    private bool optionsVisible = false;

    private const string VolumePrefKey = "GameVolume";

    private void Start()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        if (volumeControls != null)
            volumeControls.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);
        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        if (optionsButton != null)
            optionsButton.onClick.AddListener(OpenOptions);

        if (resumegame != null)
            resumegame.onClick.AddListener(CloseOptions);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    private void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumePrefKey, value);
    }

    public void OpenOptions()
    {
        optionsVisible = true;
        SetOptionsVisibility(true);
    }

    public void CloseOptions()
    {
        optionsVisible = false;
        SetOptionsVisibility(false);
    }


    private void SetOptionsVisibility(bool isVisible)
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(isVisible);

        if (volumeControls != null)
            volumeControls.SetActive(isVisible);
    }

    public void QuitGame()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }
}