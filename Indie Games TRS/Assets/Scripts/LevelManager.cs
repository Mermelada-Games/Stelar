using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] constellationLight;
    [SerializeField] GameObject[] constellationLine;
    private int levelReached;

    private void Start()
    {
        levelReached = PlayerPrefs.GetInt("levelReached", 0);

        for (int i = 0; i < constellationLight.Length; i++)
        {
            if (i + 1 > levelReached && i < constellationLight.Length)
            {
                constellationLight[i].SetActive(false);
                Debug.Log("false");
            }
            else
            {
                constellationLight[i].SetActive(true);
                Debug.Log("true");
            }
        }
        for (int i = 0; i < constellationLine.Length; i++)
        {
            if (i + 2 >levelReached && i < constellationLine.Length)
            {
                constellationLine[i].SetActive(false);
            }
            else
            {
                constellationLine[i].SetActive(true);
            }
        }
        Debug.Log(levelReached);
    }

    public void RestartProgress()
    {
        PlayerPrefs.SetInt("levelReached", 0);
        PlayerPrefs.Save();

        levelReached = 0;
        for (int i = 0; i < constellationLight.Length; i++)
        {
            constellationLight[i].SetActive(false);
        }
        for (int i = 0; i < constellationLine.Length; i++)
        {
            constellationLine[i].SetActive(false);
        }
        Debug.Log(levelReached);
    }
}
