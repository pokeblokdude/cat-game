using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] int frameCap = 60;
    [SerializeField] bool capFramerate = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject settingsMenuUI;
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
            if(settingsMenuUI.activeSelf) {
                LoadPauseMenuFromSettings();
            }
            else {
                PlayerPrefs.Save();
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1;
                paused = false;
            }
        }
        else {
            pauseMenuUI.SetActive(true);
            settingsMenuUI.SetActive(false);
            Time.timeScale = 0;
            paused = true;
        }
    }

    public void LoadPauseMenuFromSettings() {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void LoadSettingsPage() {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void Quit() {
        Application.Quit();
    }

}
