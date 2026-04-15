using UnityEngine;
using TMPro;
using System.Collections;

public class ScreenFeedback : MonoBehaviour
{
    public static ScreenFeedback Instance;

    public TMP_Text feedbackText;
    public float displayTime = 2f;

    void Awake()
    {
        Instance = this;
        feedbackText.gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(message));
    }

    IEnumerator ShowRoutine(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(displayTime);

        feedbackText.gameObject.SetActive(false);
    }
}
