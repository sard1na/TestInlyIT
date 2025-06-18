using UnityEngine;
using TMPro;
using System.Collections;

public class NoteManager : MonoBehaviour
{
    public GameObject notePanel;
    public TMP_Text noteText;
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = notePanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                Debug.LogError("NoteManager: на notePanel не найден компонент CanvasGroup.");
        }

        if (noteText == null)
        {
            noteText = notePanel.GetComponentInChildren<TMP_Text>();
            if (noteText == null)
                Debug.LogError("NoteManager: TMP_Text для записки не найден внутри notePanel.");
        }

        notePanel.SetActive(false);
    }

    public void ShowNote(string text)
    {
        noteText.text = text;
        notePanel.SetActive(true);
        StartCoroutine(FadeIn());
    }

    public void HideNote()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        canvasGroup.alpha = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        notePanel.SetActive(false);
    }
}
