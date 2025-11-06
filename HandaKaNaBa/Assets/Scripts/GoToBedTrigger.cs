using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToBedTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject bedPanel;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [Header("Questions")]
    [SerializeField]
    private string[] questions = {
        "Did you lock the doors?",
        "Did you turn off the lights?",
        "Did you unplug electronics?",
        "Did you check the windows?",
        "Are you ready to go to bed?"
    };

    [Header("Scenes")]
    [Tooltip("Name of the next scene to load if successful.")]
    [SerializeField] private string nextSceneName;

    [Tooltip("Name of the scene or state to show if the player fails.")]
    [SerializeField] private string failedSceneName;

    private int currentQuestion = -1;
    private bool playerInside = false;

    private FirstPersonController playerController;

    private void Start()
    {
        bedPanel.SetActive(false);

        yesButton.onClick.AddListener(OnYesPressed);
        noButton.onClick.AddListener(OnNoPressed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        playerController = collision.gameObject.GetComponent<FirstPersonController>();

        if (playerController != null)
        {
            playerInside = true;
            OpenFirstPrompt();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (playerController != null && collision.gameObject == playerController.gameObject)
        {
            playerInside = false;
            bedPanel.SetActive(false);
            currentQuestion = -1;
            HideCursor();
            playerController.UnlockControls();
        }
    }

    private void OpenFirstPrompt()
    {
        bedPanel.SetActive(true);
        questionText.text = "Handa Ka Na Ba?";
        currentQuestion = -1;
        ShowCursor();
        playerController?.LockControls();
    }

    private void OnYesPressed()
    {
        if (currentQuestion == -1)
        {
            currentQuestion = 0;
            ShowNextQuestion();
        }
        else
        {
            currentQuestion++;
            ShowNextQuestion();
        }
    }

    private void OnNoPressed()
    {
        if (currentQuestion == -1)
        {
            FailSequence("You weren’t ready to go to bed.");
            return;
        }

        FailSequence("You missed some preparations!");
    }

    private void ShowNextQuestion()
    {
        if (currentQuestion < questions.Length)
        {
            questionText.text = questions[currentQuestion];
        }
        else
        {
            CheckPreparedness();
        }
    }

    private void CheckPreparedness()
    {
        bedPanel.SetActive(false);
        HideCursor();
        playerController?.UnlockControls();

        if (TaskManager.Instance == null)
        {
            Debug.LogWarning("TaskManager not found in scene.");
            return;
        }

        TaskManager.Instance.CheckAllTasks();

        if (TaskManager.Instance.allTasksCompleted)
        {
            Debug.Log("? All tasks completed — going to bed safely.");
            GoToNextScene();
        }
        else
        {
            Debug.Log("? Not all tasks are completed — fail.");
            FailSequence("You forgot to finish some preparations!");
        }
    }

    private void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene not assigned in GoToBedTrigger!");
        }
    }

    private void FailSequence(string message)
    {
        Debug.Log($"FAILED: {message}");
        HideCursor();
        playerController?.UnlockControls();

        if (!string.IsNullOrEmpty(failedSceneName))
        {
            SceneManager.LoadScene(failedSceneName);
        }
        else
        {
            Debug.LogWarning("Failed scene not assigned — staying in current scene.");
        }
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
