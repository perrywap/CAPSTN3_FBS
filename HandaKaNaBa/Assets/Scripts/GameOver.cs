using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static string lastPlayedScene;

    [Header("Buttons")]
    public Button quitButton;
    public Button retryButton;

    [Header("Scenes")]
    public string quitSceneName;

    private void Awake()
    {
        // Store the previous scene automatically, as long as this isn't the GameOver scene
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != quitSceneName) // quitSceneName should be set to GameOver scene name
        {
            lastPlayedScene = currentScene;
            Debug.Log("Stored Last Scene: " + lastPlayedScene);
        }
    }

    void Start()
    {
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitToMenu);

        if (retryButton != null)
            retryButton.onClick.AddListener(Retry);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

    public void Retry()
    {
        if (!string.IsNullOrEmpty(lastPlayedScene))
        {
            Debug.Log("Retrying Scene: " + lastPlayedScene);
            SceneManager.LoadScene(lastPlayedScene);
        }
        else
        {
            Debug.LogWarning("No last scene stored!");
        }
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(quitSceneName);
    }

    public void ShowGameOverUI()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
