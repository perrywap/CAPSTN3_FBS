using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfDayManager : MonoBehaviour
{
    [System.Serializable]
    public class TaskQuestion
    {
        public TaskType taskType;
        public string question;
    }

    [SerializeField] private List<TaskQuestion> questions; // Assign in Inspector
    [SerializeField] private GameObject questionUI;        // UI panel for yes/no buttons
    [SerializeField] private TMPro.TextMeshProUGUI questionText;


    [Header("Scenes")]
    [Tooltip("Name of the next scene to load if successful.")]
    [SerializeField] private string nextSceneName;

    [Tooltip("Name of the scene to load if failed.")]
    [SerializeField] private string failedSceneName;

    private FirstPersonController playerController;

    private int currentQuestionIndex = 0;
    private bool waitingForAnswer = false;

    private void Start()
    {
        questionUI.SetActive(false);
    }

    public void StartEndOfDaySequence()
    {
        ShowCursor();

        currentQuestionIndex = 0;
        questionUI.SetActive(true);
        AskNextQuestion();
    }

    void AskNextQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            // All questions passed
            LevelComplete();
            return;
        }

        var current = questions[currentQuestionIndex];
        questionText.text = current.question;
        waitingForAnswer = true;
    }

    public void OnAnswerYes()
    {
        if (!waitingForAnswer) return;
        waitingForAnswer = false;

        var current = questions[currentQuestionIndex];
        bool actuallyComplete = TaskManager.Instance.IsTaskTypeComplete(current.taskType);

        if (!actuallyComplete)
        {
            GameFail(current.taskType);
            return;
        }

        // Task was truly complete — continue to next
        currentQuestionIndex++;
        AskNextQuestion();
    }

    public void OnAnswerNo()
    {
        if (!waitingForAnswer) return;
        waitingForAnswer = false;

        var current = questions[currentQuestionIndex];
        bool actuallyComplete = TaskManager.Instance.IsTaskTypeComplete(current.taskType);

        if (actuallyComplete)
        {
            // Player answered incorrectly (missed what they did)
            GameFail(current.taskType);
            return;
        }

        // Player correctly admitted not doing it → fail anyway (since task incomplete)
        GameFail(current.taskType);
    }

    void GameFail(TaskType failedTask)
    {
        questionUI.SetActive(false);
        //Debug.Log($"You failed at: {failedTask}. Try again!");
        // TODO: Show fail screen or restart
        FailSequence($"You failed at: {failedTask}. Try again!");
    }

    void LevelComplete()
    {
        questionUI.SetActive(false);
        Debug.Log("All tasks done correctly! You can go to bed.");
        // TODO: Trigger end sequence or next level
        GoToNextScene();
    }

    private void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("? No next scene assigned in GoToBedTrigger!");
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
            Debug.LogWarning("? Failed scene not assigned — staying in current scene.");
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
