using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioSystem : MonoBehaviour
{
    [SerializeField] private Slider fxSlider;

    [SerializeField] private Slider musicSlider;
    private AudioSource  audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
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
        audioSource.volume = fxSlider.value;
    }

      private void SetMusicVolume(float volume)
    {
        audioSource.volume = musicSlider.value;
    }
}