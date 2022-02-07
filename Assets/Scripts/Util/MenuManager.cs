using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] int frameCap = 60;
    [SerializeField] bool capFramerate = false;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject settingsMenuUI;
    TransitionUI transitionUI;
    AudioManager audioManager;
    Player player;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        transitionUI = FindObjectOfType<TransitionUI>();
        menuCanvas.SetActive(true);
        settingsMenuUI.SetActive(false);
        StartCoroutine(LevelLoadDelay());
    }

    IEnumerator LevelLoadDelay() {
        yield return new WaitForSeconds(0.5f);
        transitionUI.PlayEnter();
    }

    void Update() {
        if(capFramerate) {
            Application.targetFrameRate = frameCap;
        }
        else {
            Application.targetFrameRate = -1;
        }
    }

    public void StartGame() {
        StartCoroutine(Wait());
    }

    IEnumerator Wait() {
        PlayerPrefs.Save();
        transitionUI.Play();
        audioManager.FadeMusic();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(1);
    }

    public void LoadMenuFromSettings() {
        settingsMenuUI.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void LoadSettingsPage() {
        menuCanvas.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void Quit() {
        Application.Quit();
    }

}

