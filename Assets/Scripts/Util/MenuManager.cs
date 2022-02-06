using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] int frameCap = 60;
    [SerializeField] bool capFramerate = false;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject settingsMenuUI;
    [SerializeField] TransitionUI transitionUI;
    AudioManager audioManager;
    Player player;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        menuCanvas.SetActive(true);
        settingsMenuUI.SetActive(false);
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
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(2.5f);
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

