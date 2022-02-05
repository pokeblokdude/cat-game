using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    
    InputManager input;

    public float moveDir { get; private set; }
    public bool jump { get; private set; }
    public bool interact { get; private set; }

    void Awake() {
        input = new InputManager();

        input.Player.Move.performed += ctx => {
            moveDir = Mathf.Sign(ctx.ReadValue<float>());
        };
        input.Player.Move.canceled += ctx => {
            moveDir = 0;
        };
        input.Player.Jump.performed += ctx => {
            jump = true;
        };
        input.Player.Jump.canceled += ctx => {
            jump = false;
        };
        input.Player.Interact.performed += ctx => {
            interact = true;
        };
        input.Player.Interact.canceled += ctx => {
            interact = false;
        };
    }

    void OnEnable() {
        input.Enable();
    }
    void OnDisable() {
        input.Disable();
    }

}
