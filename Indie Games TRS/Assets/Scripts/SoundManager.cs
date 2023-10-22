using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSource, fxSource;
    private void Awake() {

        musicSource.volume = 1;
        fxSource.volume = 1;
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip){
        fxSource.PlayOneShot(clip);
    }

    public AudioSource GetMusicSource(){
        return musicSource;
    }

      public AudioSource GetFxSource(){
          return fxSource;
    }
}
