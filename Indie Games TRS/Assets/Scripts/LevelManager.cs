using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] constellationLine;
    private int levelReached;

    private void Start()
    {
        levelReached = PlayerPrefs.GetInt("levelReached", 0);

        for (int i = 0; i < constellationLine.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                constellationLine[i].SetActive(false);
            }
            else
            {
                constellationLine[i].SetActive(true);
            }
        }
    }

    public void RestartProgress()
    {
        PlayerPrefs.SetInt("levelReached", 0);
        PlayerPrefs.Save();

        levelReached = 1;
        for (int i = 0; i < constellationLine.Length; i++)
        {
            constellationLine[i].SetActive(false);
        }
    }
}
