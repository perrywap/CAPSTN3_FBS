using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Setup")]
    [SerializeField] private Object startScene;

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
        if (startScene == null)
        {
            Debug.LogError("Start Scene not assigned in the Inspector!");
            return;
        }
        Time.timeScale = 1f;
#if UNITY_EDITOR
        string scenePath = AssetDatabase.GetAssetPath(startScene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SceneManager.LoadScene(sceneName);
#else
        SceneManager.LoadScene(startScene.name);
#endif
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game from Main Menu...");
        Time.timeScale = 1f;
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenOptions()
    {
        SetOptionsVisible(true);
    }

    public void CloseOptions()
    {
        SetOptionsVisible(false);
    }

    public void SetOptionsVisible(bool isVisible)
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(isVisible);
        }
    }
}