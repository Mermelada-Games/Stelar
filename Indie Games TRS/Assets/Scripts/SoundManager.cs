using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource[] musicSources, fxSources;

    private void Awake()
    {
        for (int i = 0; i < musicSources.Length; i++)
        {
            musicSources[i].volume = 1;
        }
        for (int i = 0; i < fxSources.Length; i++)
        {
            fxSources[i].volume = 1;
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource[] GetMusicSources()
    {
        return musicSources;
    }

    public AudioSource[] GetFxSources()
    {
        return fxSources;
    }

    public void PopFx()
    {
        fxSources[0].Play();
    }

    public void SplatFx()
    {
        fxSources[1].Play();
    }
    public void Glassfx()
    {
        fxSources[2].Play();
    }

    public void WinFx()
    {
        fxSources[3].Play();
    }

    public void StarFx()
    {
        fxSources[4].Play();
    }
    
    public void PlayMenu()
    {
        musicSources[0].Play();
    }

    public void PlayMusic()
    {
        musicSources[1].Play();
    }

    public void PauseMusic()
    {
        musicSources[0].Pause();
        musicSources[1].Pause();
    }
}
