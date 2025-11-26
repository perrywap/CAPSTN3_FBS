using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Setup")]
    [SerializeField] private string startSceneName;

    [Header("UI Panels")]
    [SerializeField] private GameObject optionsPanel;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        if (optionsButton != null)
            optionsButton.onClick.AddListener(OpenOptions);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(startSceneName))
        {
            Debug.LogError("Start Scene Name not assigned!");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(startSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game from Main Menu...");
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void OpenOptions() => SetOptionsVisible(true);
    public void CloseOptions() => SetOptionsVisible(false);

    private void SetOptionsVisible(bool visible)
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(visible);
    }
}