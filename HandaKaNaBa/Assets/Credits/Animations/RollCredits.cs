using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollCredits : MonoBehaviour
{
    [SerializeField] private string MainMenuScene = "MainMenu";

    private void Start()
    {
        Invoke("ReturnToMainMenu", 25f);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }
}
