using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    [SerializeField] TransitionUI transitionUI;

    public void ReloadScene() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transitionUI.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
