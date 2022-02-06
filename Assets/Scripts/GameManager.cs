using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] int frameCap = 60;
    [SerializeField] bool capFramerate = false;
    [SerializeField] GameObject pauseMenuUI;
    AudioSource musicSource;

    PlayerInput input;
    bool paused = false;
    bool pausing = false;

    void Start() {
        input = FindObjectOfType<PlayerInput>();
        musicSource = GetComponent<AudioSource>();
        pauseMenuUI.SetActive(false);
    }

    void Update() {
        if(capFramerate) {
            Application.targetFrameRate = frameCap;
        }
        else {
            Application.targetFrameRate = -1;
        }
        if(input.pause && !pausing) {
            Pause();
            pausing = true;
        }
        if(!input.pause) {
            pausing = false;
        }
    }

    public void Pause() {
        if(paused) {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
        else {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void UpdateMusicVolume(float vol) {
        musicSource.volume = vol / 100;
        PlayerPrefs.SetFloat("MusicVolume", vol / 100);
    }
}
