using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassableFloor : MonoBehaviour {
    
    PlayerInput input;
    MeshCollider col;
    float turnedOffTimestamp;
    bool playerIsStandingOn = false;

    void Start() {
        col = GetComponent<MeshCollider>();
        input = FindObjectOfType<PlayerInput>();
    }

    void Update() {
        if(playerIsStandingOn) {
            print("player standing");
        }
        if(input.down && input.jump && playerIsStandingOn) {
            col.enabled = false;
            turnedOffTimestamp = Time.time;
        }
        if(col.enabled == false && Time.time - turnedOffTimestamp > 0.4f) {
            col.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Player")) {
            playerIsStandingOn = true;
        }
    }
    void OnCollisionExit(Collision collision) {
        playerIsStandingOn = false;
    }
}
