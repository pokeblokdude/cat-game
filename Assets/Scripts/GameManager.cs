using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] int frameCap = 60;
    [SerializeField] bool capFramerate = false;

    PlayerInput input;

    void Start() {
        input = FindObjectOfType<PlayerInput>();
    }

    void Update() {
        if(capFramerate) {
            Application.targetFrameRate = frameCap;
        }
        else {
            Application.targetFrameRate = -1;
        }
        if(input.quit) {
            Application.Quit();
        }
    }
}
