using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    [Header("Volume Controls")]
    [SerializeField] private GameObject volumeControls;
    [SerializeField] private Slider volumeSlider; 

    [Header("Player")]
    [SerializeField] private FirstPersonController playerController;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitToMainMenuButton;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;
    private bool optionsVisible = false;

    private const string VolumePrefKey = "GameVolume"; 

    private void Start()
    {
        pausePanel.SetActive(false);
        volumeControls.SetActive(false);

        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);
        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged); 
        }

        resumeButton.onClick.AddListener(TogglePause);
        optionsButton.onClick.AddListener(ToggleOptions);
        quitToMainMenuButton.onClick.AddListener(QuitToMainMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    private void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = false; 

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerController?.LockControls();
    }

    private void ResumeGame()
    {
        isPaused = false;

        pausePanel.SetActive(false);
        volumeControls.SetActive(false);
        optionsVisible = false;

        Time.timeScale = 1f;
        AudioListener.pause = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerController?.UnlockControls();
    }

    private void ToggleOptions()
    {
        optionsVisible = !optionsVisible;
        volumeControls.SetActive(optionsVisible);
    }

    private void OnVolumeChanged(float value) 
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumePrefKey, value);
    }

    private void QuitToMainMenu()
    {
        Debug.Log("QUIT BUTTON PRESSED!"); 
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (!string.IsNullOrEmpty("MainMenu"))
            SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
