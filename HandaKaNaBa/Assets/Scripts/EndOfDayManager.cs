using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfDayManager : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public class TaskQuestion
    {
        public TaskType taskType;
        public string question;
    }

    [SerializeField] private List<TaskQuestion> questions;
    [SerializeField] private GameObject questionUI;
    [SerializeField] private TMPro.TextMeshProUGUI questionText;

    [Header("UI References")]
    [SerializeField] private GameObject failUI;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Scenes")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private string quitSceneName;

    private FirstPersonController playerController;
    private int currentQuestionIndex = 0;
    private bool waitingForAnswer = false;
    private string lastPlayedScene;
    private CanvasGroup failCanvasGroup;

    private void Start()
    {
        questionUI.SetActive(false);
        if (failUI != null)
        {
            failUI.SetActive(false);
            failCanvasGroup = failUI.GetComponent<CanvasGroup>();
            if (failCanvasGroup == null)
                failCanvasGroup = failUI.AddComponent<CanvasGroup>();
            failCanvasGroup.alpha = 0f;
            failCanvasGroup.interactable = true;
            failCanvasGroup.blocksRaycasts = true;
        }

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitToMenu);

        if (retryButton != null)
            retryButton.onClick.AddListener(Retry);

        lastPlayedScene = SceneManager.GetActiveScene().name;
    }

    public void Interact()
    {
        StartEndOfDaySequence();
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
            GameFail(current.taskType);
            return;
        }

        GameFail(current.taskType);
    }

    void GameFail(TaskType failedTask)
    {
        questionUI.SetActive(false);
        FailSequence();
    }

    void LevelComplete()
    {
        questionUI.SetActive(false);
        GoToNextScene();
    }

    private void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void FailSequence()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        playerController?.UnlockControls();

        if (failUI != null)
        {
            failUI.SetActive(true);
            if (failCanvasGroup != null)
            {
                failCanvasGroup.alpha = 0f;
                failCanvasGroup.interactable = true;
                failCanvasGroup.blocksRaycasts = true;
            }
            StartCoroutine(FadeInFailUI());
        }
    }

    private IEnumerator FadeInFailUI()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            if (failCanvasGroup != null)
                failCanvasGroup.alpha = alpha;
            yield return null;
        }
        if (failCanvasGroup != null)
            failCanvasGroup.alpha = 1f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(lastPlayedScene))
        {
            SceneManager.LoadScene(lastPlayedScene);
        }
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(quitSceneName))
        {
            SceneManager.LoadScene(quitSceneName);
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
