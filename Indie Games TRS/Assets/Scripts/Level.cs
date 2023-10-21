using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] GameObject[] stars;
    public int starsCount = 0;
    private bool starsCollected = false;
    public bool endCell = false;

    private void Update()
    {
        if (starsCount == stars.Length)
        {
            starsCollected = true;
        }

        if (starsCollected && endCell)
        {
            ChangeLevel();
        }
    }

    private void ChangeLevel()
    {
        Debug.Log("LEVEL COMPLETED!");
        PlayerPrefs.SetInt("levelReached", 2);
        PlayerPrefs.Save();

        SceneManager.LoadScene("SelectLevelScene");
    }
}
