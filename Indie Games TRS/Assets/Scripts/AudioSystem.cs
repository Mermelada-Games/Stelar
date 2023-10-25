using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSystem : MonoBehaviour
{
  private Slider fxSlider;
  private Slider musicSlider;
  private SoundManager soundManager;

  private bool musicMuted = false;
  private bool fxMuted = false;

  private void Start()
  {
    soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    AudioSource[] musicSources = soundManager.GetMusicSources();
    AudioSource[] fxSources = soundManager.GetFxSources();
    fxSlider = GameObject.FindGameObjectWithTag("FxSlider").GetComponent<Slider>();
    musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
    fxSlider.onValueChanged.AddListener(delegate { OnFxSliderValueChanged(); });
    musicSlider.onValueChanged.AddListener(delegate { OnMusicSliderValueChanged(); });
  }

  private void OnFxSliderValueChanged()
  {
    if (!fxMuted)
    {
      SetFxVolume(fxSlider.value);
    }
  }

  private void OnMusicSliderValueChanged()
  {
    if (!musicMuted)
    {
      SetMusicVolume(musicSlider.value);
    }
  }

  private void SetFxVolume(float volume)
  {
    foreach (AudioSource fxSource in soundManager.GetFxSources())
    {
      fxSource.volume = volume;
    }
  }

  private void SetMusicVolume(float volume)
  {
    foreach (AudioSource musicSource in soundManager.GetMusicSources())
    {
      musicSource.volume = volume;
    }
  }

  public void ToggleMusicMute()
  {
    musicMuted = !musicMuted;

    foreach (AudioSource musicSource in soundManager.GetMusicSources())
    {
      musicSource.mute = musicMuted;
    }

    if (musicMuted)
    {
      musicSlider.value = 0f;
    }
    else
    {
      musicSlider.value = soundManager.GetMusicSources()[0].volume;
    }
  }

  public void ToggleFxMute()
  {
    fxMuted = !fxMuted;

    foreach (AudioSource fxSource in soundManager.GetFxSources())
    {
      fxSource.mute = fxMuted;
    }

    if (fxMuted)
    {
      fxSlider.value = 0f;
    }
    else
    {
      fxSlider.value = soundManager.GetFxSources()[0].volume;
    }
  }
}
