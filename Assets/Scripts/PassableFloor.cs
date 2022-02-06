using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassableFloor : MonoBehaviour {
    
    Player player;
    PlayerInput input;
    MeshCollider col;
    float turnedOffTimestamp;
    bool playerIsStandingOn = false;

    float playerOffsetX, playerOffsetY;

    void Start() {
        col = GetComponent<MeshCollider>();
        player = FindObjectOfType<Player>();
        input = FindObjectOfType<PlayerInput>();
    }

    void Update() {
        playerOffsetX = Mathf.Abs(player.transform.position.x - transform.position.x);
        playerOffsetY = Mathf.Abs(player.transform.position.y - transform.position.y);

        if(!playerIsStandingOn) {
            if(playerOffsetY < 0.1f && playerOffsetX < col.bounds.extents.x) {
                playerIsStandingOn = true;    
            }
        }
        if(playerOffsetY > 0.1f || playerOffsetX > col.bounds.extents.x) {
            playerIsStandingOn = false;    
        }
        if(playerIsStandingOn) {
            //print(name + "player standing");
        }
        if(input.down && input.jump && playerIsStandingOn) {
            col.enabled = false;
            turnedOffTimestamp = Time.time;
        }
        if(col.enabled == false && Time.time - turnedOffTimestamp > 0.3f) {
            col.enabled = true;
        }
    }
}
