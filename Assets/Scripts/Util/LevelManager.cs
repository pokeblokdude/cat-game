using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    [SerializeField] TransitionUI transitionUI;

    void Awake() {
        transitionUI.gameObject.SetActive(true);
    }

    public void ReloadScene() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transitionUI.Play();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
