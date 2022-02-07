using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    TransitionUI transitionUI;

    void Awake() {
        transitionUI = FindObjectOfType<TransitionUI>();
        transitionUI.gameObject.SetActive(true);
    }

    void Start() {
        StartCoroutine(LevelLoadDelay());
    }

    IEnumerator LevelLoadDelay() {
        yield return new WaitForSeconds(0.5f);
        transitionUI.PlayEnter();
    }

    public void LoadMenu() {
        StartCoroutine(LoadLevel(0, 2.5f));
    }

    public void ReloadScene() {
        StartCoroutine(LoadLevel(1, 2.5f));
    }

    IEnumerator LoadLevel(int index, float time) {
        transitionUI.Play();
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(index);
    }

}
