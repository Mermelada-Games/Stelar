using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;
    [SerializeField] private UIStar[] uiStars;
    [SerializeField] private float changeLevelDelay =  5f;
    public int starsCount = 0;
    private bool starsCollected = false;
    private bool levelCompleted = false;
    public bool endCell = false;
    public Line lineScript;

    private void Update()
    {
        if (starsCount == stars.Length)
        {
            starsCollected = true;
        }

        if (starsCollected && endCell && !levelCompleted)
        {
            levelCompleted = true;
            ChangeLevelWithDelay();
        }
    }

    private void ChangeLevelWithDelay()
    {
        PlayerPrefs.SetInt("levelReached", 2);
        PlayerPrefs.Save();
        StartCoroutine(lineScript.LineAnimation());
        StartCoroutine(ChangeLevelAfterDelay());
    }

    private IEnumerator ChangeLevelAfterDelay()
    {
        yield return new WaitForSeconds(changeLevelDelay);

        SceneManager.LoadScene("SelectLevelScene");
    }

    public void CollectStar(int starIndex)
    {
        if (starIndex >= 0 && starIndex < uiStars.Length)
        {
            uiStars[starIndex].CollectStar();
        }
    }
}
