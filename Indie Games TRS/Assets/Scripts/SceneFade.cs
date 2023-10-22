using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    public float fadeDuration = 1.0f;
    private bool isFading = false;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        fadeImage.color = Color.black;
        Color originalColor = fadeImage.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = Time.time - startTime;
            float t = elapsedTime / fadeDuration;
            fadeImage.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }
    }

    public IEnumerator FadeOutAndLoad(string sceneName)
    {
        isFading = true;
        fadeImage.color = Color.clear;
        Color originalColor = fadeImage.color;
        Color targetColor = Color.black;

        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = Time.time - startTime;
            float t = elapsedTime / fadeDuration;
            fadeImage.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
