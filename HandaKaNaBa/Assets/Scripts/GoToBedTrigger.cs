using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
        "Did you lock the door?",
        "Did you turn off the lights?",
        "Did you close the windows?",
    };

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 2f;

    private int currentQuestion = -1;
    private bool playerInside = false;
    private bool isFading = false;

    private void Start()
    {
        bedPanel.SetActive(false);
        fadeOverlay.color = new Color(0, 0, 0, 0);

        yesButton.onClick.AddListener(OnYesPressed);
        noButton.onClick.AddListener(OnNoPressed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        FirstPersonController player = collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            playerInside = true;
            OpenFirstPrompt();
        }
    }

    private void OnTriggerExit(Collider collision)
    {

        FirstPersonController player = collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            playerInside = false;
            bedPanel.SetActive(false);
            currentQuestion = -1;
        }
    }

    private void OpenFirstPrompt()
    {
        
        bedPanel.SetActive(true);
        questionText.text = "Go to bed?";
        currentQuestion = -1;
    }

    private void OnYesPressed()
    {
        if (currentQuestion == -1)
        {
            // Start the question sequence
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
            return;
        }

        // If during questions, continue to next question anyway
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
            // All questions done
            StartCoroutine(FadeToBlack());
        }
    }

    private IEnumerator FadeToBlack()
    {
        bedPanel.SetActive(false);
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

        // Load Scene or Cutscene here
    }
}
