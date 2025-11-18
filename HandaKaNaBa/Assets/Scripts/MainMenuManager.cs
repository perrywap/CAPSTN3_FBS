using NUnit.Framework;
using System.Collections.Generic;
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

    [Header("Menu Buttons Sprites")]
    [SerializeField] private Image lampPostSprite;
    [SerializeField] private List<Sprite> buttonSprites;
    
    private int index = 0;

    private void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        
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

    public void OnStartButtonClicked()
    {
        lampPostSprite.sprite = buttonSprites[1];
    }

    public void OnOptionButtonClicked()
    {
        lampPostSprite.sprite = buttonSprites[2];
    }

    public void OnQuitButtonClicked()
    {
        lampPostSprite.sprite = buttonSprites[3];
    }
}
