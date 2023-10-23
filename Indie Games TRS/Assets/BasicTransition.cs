using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine SceneManagement;


public class BasicTransition : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SettingsCoroutine();
        }
    }

    public void SettingsCoroutine()
    {
        StartCorourtine(LoadLevel(SceneManagement.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
    
}
