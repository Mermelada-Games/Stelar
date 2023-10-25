using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;
    [SerializeField] private int levelIdx = 0;
    [SerializeField] private float changeLevelDelay =  5f;
    [SerializeField] private bool hasAnimation = true;
    public int starsCount = 0;
    private bool starsCollected = false;
    private bool levelCompleted = false;
    public bool endCell = false;
    public Line lineScript;
    private SceneFade sceneFade;

    private void Start()
    {
        sceneFade = FindObjectOfType<SceneFade>();
    }
    
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
        PlayerPrefs.SetInt("levelReached" + levelIdx, levelIdx);
        PlayerPrefs.Save();
        if (hasAnimation) StartCoroutine(lineScript.LineAnimation());
        StartCoroutine(ChangeLevelAfterDelay());
    }

    private IEnumerator ChangeLevelAfterDelay()
    {
        yield return new WaitForSeconds(changeLevelDelay);
        sceneFade.EndLevel("Galaxy");
    }

    public void RestartLevel()
    {
        foreach (GameObject star in stars)
        {
            star.GetComponent<Star>().RestartStar();
        }
    }
}
