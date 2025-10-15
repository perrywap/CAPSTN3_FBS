using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; 
#endif

public class GoToBedTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject bedPanel;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Image fadeOverlay;

    [Header("Questions")]
    [SerializeField]
    private string[] questions = {
        "Did you lock the doors?",
        "Did you turn off the lights?",
        "Did you unplug electronics?",
        "Did you check the windows?",
        "Are you ready to go to bed?"
    };

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 2f;

    [Header("Next Scene")]
#if UNITY_EDITOR
    [SerializeField] private SceneAsset nextScene; 
#endif
    private string nextSceneName;

    private int currentQuestion = -1;
    private bool playerInside = false;
    private bool isFading = false;

    private FirstPersonController playerController;

    private void Start()
    {
        bedPanel.SetActive(false);
        fadeOverlay.color = new Color(0, 0, 0, 0);

        yesButton.onClick.AddListener(OnYesPressed);
        noButton.onClick.AddListener(OnNoPressed);

#if UNITY_EDITOR
        if (nextScene != null)
            nextSceneName = nextScene.name;
#endif
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
            bedPanel.SetActive(false);
            HideCursor();
            playerController?.UnlockControls();
            return;
        }

        currentQuestion++;
        ShowNextQuestion();
    }

    private void ShowNextQuestion()
    {
        if (currentQuestion < questions.Length)
        {
            questionText.text = questions[currentQuestion];
        }
        else
        {
            StartCoroutine(FadeToBlack());
        }
    }

    private IEnumerator FadeToBlack()
    {
        bedPanel.SetActive(false);
        HideCursor();
        playerController?.UnlockControls();
        isFading = true;

        float elapsed = 0f;
        Color color = fadeOverlay.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            fadeOverlay.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        Debug.Log("Player is now asleep...");

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No next scene assigned in GoToBedTrigger.");
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
