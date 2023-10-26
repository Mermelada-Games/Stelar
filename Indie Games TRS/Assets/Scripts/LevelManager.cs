using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] constellationLight;
    [SerializeField] GameObject[] constellationLine;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private int levelIdx;
    [SerializeField] private int[] levelReached;
    private int[] savedLevelReached;

    private void Start()
    {
        for (int i = 1; i < levelReached.Length; i++)
        {
            levelIdx = PlayerPrefs.GetInt("levelReached" + i, 0);
            if (levelIdx == i)
            {
                levelReached[i] = 1;
            }
        }

        for (int i = 0; i < stars.Length; i++)
        {
            if (levelReached[1] == 1)
            {
                stars[i].SetActive(true);
            }
            else
            {
                stars[i].SetActive(false);
            }
        }

        for (int i = 1; i < constellationLight.Length + 1; i++)
        {
            if (levelReached[i] == 1)
            {
                constellationLight[i - 1].SetActive(true);
            }
            else
            {
                constellationLight[i - 1].SetActive(false);
            }
        }
        for (int i = 2; i < constellationLine.Length + 2; i++)
        {
            if (levelReached[i] == 1)
            {
                constellationLine[i - 2].SetActive(true);
            }
            else
            {
                constellationLine[i - 2].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RestartProgress();
        }
    }

    public void RestartProgress()
    {

        for (int i = 0; i < levelReached.Length; i++)
        {
            PlayerPrefs.SetInt("levelReached" + i, 0);
            PlayerPrefs.Save();
            levelReached[i] = 0;
        }
        for (int i = 0; i < stars.Length; i++)
        {
            {
                stars[i].SetActive(false);
            }
        }
        for (int i = 0; i < constellationLight.Length; i++)
        {
            constellationLight[i].SetActive(false);
        }
        for (int i = 0; i < constellationLine.Length; i++)
        {
            constellationLine[i].SetActive(false);
        }
    }
}
