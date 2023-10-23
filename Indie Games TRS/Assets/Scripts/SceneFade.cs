using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private bool startTransition = false;
    [SerializeField] private bool fadeIn = false;
    public float fadeDuration = 1.0f;
    private string sceneName;

    [SerializeField] private Animator transition;

    private void Start()
    {
        if (fadeIn) StartCoroutine(FadeIn());
        if (startTransition) transition.SetTrigger("Start");
    }

    public IEnumerator FadeIn()
    {
        fadeImage.enabled = true;
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
        fadeImage.enabled = false;
    }

    public IEnumerator FadeOutAndLoad(string sceneName)
    {
        fadeImage.enabled = true;
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

    public void EndLevel(string scene)
    {
        transition.SetTrigger("End");
        sceneName = scene;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
