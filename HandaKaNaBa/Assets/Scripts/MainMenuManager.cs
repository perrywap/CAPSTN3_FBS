using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene to Load When Starting Game")]
    [SerializeField] private Object startScene; 

    [Header("UI Buttons (Optional)")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

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

#if UNITY_EDITOR
        string scenePath = UnityEditor.AssetDatabase.GetAssetPath(startScene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SceneManager.LoadScene(sceneName);
#else
        SceneManager.LoadScene(startScene.name);
#endif
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
