using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

    [SerializeField] int frameCap = 60;
    [SerializeField] bool capFramerate = false;
    [SerializeField] GameObject creditsCanvas;
    TransitionUI transitionUI;
    AudioManager audioManager;

    CreditsSequencer sequencer;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        transitionUI = FindObjectOfType<TransitionUI>();
        sequencer = FindObjectOfType<CreditsSequencer>();
        creditsCanvas.SetActive(true);
        StartCoroutine(LevelLoadDelay());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator LevelLoadDelay() {
        yield return new WaitForSeconds(0.5f);
        transitionUI.PlayEnter();
        sequencer.StartSequence();
    }

    void Update() {
        if(capFramerate) {
            Application.targetFrameRate = frameCap;
        }
        else {
            Application.targetFrameRate = -1;
        }
    }

    public void LoadMenu() {
        StartCoroutine(Wait(0));
    }

    IEnumerator Wait(int index) {
        transitionUI.Play();
        audioManager.FadeMusic();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(index);
    }

}

