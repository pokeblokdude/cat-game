using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    
    List<AudioSource> soundEffects;
    AudioSource music;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    void Awake() {
        soundEffects = new List<AudioSource>();
        AudioSource[] sounds = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sounds.Length; i++) {
            if(!sounds[i].CompareTag("Music")) {
                soundEffects.Add(sounds[i]);
            }
        }
        music = GetComponent<AudioSource>();

        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        masterVolumeSlider.value = masterVolume * 100;
        AudioListener.volume = masterVolume;
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicVolumeSlider.value = musicVolume * 100;
        music.volume = musicVolume;
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        sfxVolumeSlider.value = sfxVolume * 100;
        foreach(AudioSource sound in soundEffects) {
            sound.volume = sfxVolume;
        }
    }

    void Start() {
        
    }

    public void UpdateMasterVolume(float vol) {
        AudioListener.volume = vol / 100;
        PlayerPrefs.SetFloat("MasterVolume", vol / 100);
    }
    public void UpdateMusicVolume(float vol) {
        music.volume = vol / 100;
        PlayerPrefs.SetFloat("MusicVolume", vol / 100);
    }
    public void UpdateSfxVolume(float vol) {
        foreach(AudioSource sound in soundEffects) {
            sound.volume = vol / 100;
        }
        PlayerPrefs.SetFloat("SfxVolume", vol / 100);
    }
}