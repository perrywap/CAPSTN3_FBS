using UnityEngine;
using TMPro;
using System.Collections;

public class PopUp : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Timing")]
    [SerializeField] private float minDelay = 10f;
    [SerializeField] private float maxDelay = 20f;
    [SerializeField] private float fadeDuration = 0.7f;
    [SerializeField] private float displayDuration = 6f;

    [Header("Messages")]
    [SerializeField]
    private string[] warningMessages = {
        "Mukhang mag-brownout mamaya...",
        "Baka pasukin ng baha ang sala...",
        "Parang may kulang pa sa emergency kit ko...",
        "Dumidilim ang ulap sa labas...",
        "Naririnig ko na ang malakas na hangin..."
    };

    private void Start()
    {
        canvasGroup.alpha = 0f;
        StartCoroutine(PopupRoutine());
    }

    private IEnumerator PopupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            string message = warningMessages[Random.Range(0, warningMessages.Length)];
            warningText.text = message;

            yield return FadeIn();
            yield return new WaitForSeconds(displayDuration);
            yield return FadeOut();
        }
    }

    private IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }
    }
}