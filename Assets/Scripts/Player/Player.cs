using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EntityController))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour {

    [SerializeField] Text text;

    [SerializeField] EntityPhysicsData playerData;
    PlayerInput input;
    EntityController controller;
    
    Vector3 wishVelocity;
    Vector3 actualVelocity;

    bool holdingJump = false;

    void Awake() {
        input = GetComponent<PlayerInput>();
        controller = GetComponent<EntityController>();

        wishVelocity = Vector3.zero;
        actualVelocity = Vector3.zero;
    }

    void Update() {
        if(!input.jump) {
            holdingJump = false;
        }
        wishVelocity = new Vector3(input.moveDir * playerData.moveSpeed, 0, 0);

        SetDebugText();
    }

    void FixedUpdate() {
        if(controller.isGrounded() && input.jump && !holdingJump) {
            controller.Jump();
            holdingJump = true;
        }
        actualVelocity = controller.Move(wishVelocity);
    }

    void SetDebugText() {
        text.text = $"FPS: {(1/Time.deltaTime).ToString("f0")}\n" +
                    $"deltaTime: {Time.deltaTime.ToString("f8")}\n\n" +
                    $"Position: {transform.position.ToString("f4")}\n" +
                    $"HSpeed: {actualVelocity.x.ToString("f2")}\n" +
                    $"VSpeed: {actualVelocity.y.ToString("f2")}\n" +
                    $"Velocity: {actualVelocity.ToString("f2")}\n" +
                    $"WishVel: {wishVelocity.ToString("f3")}\n" +
                    $"Grounded: {controller.isGrounded()}"
        ;
    }
}
