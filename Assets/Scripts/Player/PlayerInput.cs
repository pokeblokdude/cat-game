using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    
    InputManager input;

    #region Player Input
    public float moveDir { get; private set; }
    public bool crouch { get; private set; }
    public bool jump { get; private set; }
    public bool down { get; private set; }
    public bool interact { get; private set; }
    #endregion

    #region Game Input
    public bool quit { get; private set; }
    #endregion

    void Awake() {
        input = new InputManager();

        #region Player
        input.Player.Move.performed += ctx => {
            moveDir = Mathf.Sign(ctx.ReadValue<float>());
        };
        input.Player.Move.canceled += ctx => {
            moveDir = 0;
        };
        input.Player.Crouch.performed += ctx => {
            crouch = true;
        };
        input.Player.Crouch.canceled += ctx => {
            crouch = false;
        };
        input.Player.Jump.performed += ctx => {
            jump = true;
        };
        input.Player.Jump.canceled += ctx => {
            jump = false;
        };
        input.Player.Down.performed += ctx => {
            down = true;
        };
        input.Player.Down.canceled += ctx => {
            down = false;
        };
        input.Player.Interact.performed += ctx => {
            interact = true;
        };
        input.Player.Interact.canceled += ctx => {
            interact = false;
        };
        #endregion

        #region Game
        input.Game.Quit.performed += ctx => {
            quit = true;
        };
        input.Game.Quit.canceled += ctx => {
            quit = false;
        };
        #endregion
    }

    void OnEnable() {
        input.Enable();
    }
    void OnDisable() {
        input.Disable();
    }

}
