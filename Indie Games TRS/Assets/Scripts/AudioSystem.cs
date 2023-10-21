using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioSystem : MonoBehaviour
{
    private Slider slider;

    private AudioSource  audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }
    private void OnSliderValueChanged()
    {
        SetVolume(slider.value);
    }
    private void SetVolume(float volume)
    {
        audioSource.volume = slider.value;
    }
}
    
