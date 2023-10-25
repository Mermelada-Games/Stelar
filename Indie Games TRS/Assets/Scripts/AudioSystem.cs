using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSystem : MonoBehaviour
{
  private Slider fxSlider;
  private Slider musicSlider;
  private SoundManager soundManager;

  private void Start()
  {
    soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    AudioSource[] musicSources = soundManager.GetMusicSources();
    AudioSource[] fxSources = soundManager.GetFxSources();
    fxSlider = GameObject.FindGameObjectWithTag("FxSlider").GetComponent<Slider>();
    musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent <Slider>();
    fxSlider.onValueChanged.AddListener(delegate { OnFxSliderValueChanged(); });
    musicSlider.onValueChanged.AddListener(delegate { OnMusicSliderValueChanged(); });
  }

  private void OnFxSliderValueChanged()
  {
    SetFxVolume(fxSlider.value);
  }

  private void OnMusicSliderValueChanged()
  {
    SetMusicVolume(musicSlider.value);
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
    foreach (AudioSource musicSource in soundManager.GetMusicSources())
    {
      musicSource.mute = !musicSource.mute;
    }
  }

  public void ToggleFxMute()
  {
    foreach (AudioSource fxSource in soundManager.GetFxSources())
    {
      fxSource.mute = !fxSource.mute;
    }
  }
}
