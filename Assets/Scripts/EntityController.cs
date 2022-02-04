using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EntityController : MonoBehaviour {
    
    [SerializeField] EntityPhysicsData entityPhysicsData;
    CharacterController controller;

    void Awake() {
        controller = GetComponent<CharacterController>();
    }

    public Vector3 Move(Vector3 moveAmount) {

        controller.Move(moveAmount);

        return controller.velocity;
    }


    public bool isGrounded() {
        return controller.isGrounded;
    }

}
