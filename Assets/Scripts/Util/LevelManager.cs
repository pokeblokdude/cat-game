using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    [SerializeField] TransitionUI transitionUI;

    void Awake() {
        transitionUI.gameObject.SetActive(true);
    }

    public void LoadMenu() {
        StartCoroutine(LoadLevel(0));
    }

    public void ReloadScene() {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int index) {
        transitionUI.Play();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(index);
    }

}
