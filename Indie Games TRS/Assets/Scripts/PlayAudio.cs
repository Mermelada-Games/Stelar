using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        AudioSource[] musicSources = soundManager.GetMusicSources();
        AudioSource[] fxSources = soundManager.GetFxSources();
    }

    public void PopFx()
    {
        soundManager.PopFx();
    }

    public void SplatFx()
    {
        soundManager.SplatFx();
    }

    public void GlassFx()
    {
        soundManager.Glassfx();
    }

}