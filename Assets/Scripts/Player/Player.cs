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

    void Awake() {
        input = GetComponent<PlayerInput>();
        controller = GetComponent<EntityController>();

        wishVelocity = Vector3.zero;
        actualVelocity = Vector3.zero;
    }

    void Update() {
        wishVelocity = new Vector3(input.moveDir * playerData.moveSpeed, actualVelocity.y, 0);

        CalculateGravity();

        if(controller.isGrounded() && input.jump) {
            wishVelocity.y += playerData.jumpForce;
        }

        actualVelocity = controller.Move(wishVelocity * Time.deltaTime);

        SetDebugText();
    }

    void CalculateGravity() {
        if(controller.isGrounded()) {
            wishVelocity.y = -playerData.gravity * Time.deltaTime;
        }
        else {
            wishVelocity.y -= playerData.gravity * Time.deltaTime;
        }
    }

    void SetDebugText() {
        text.text = $"FPS: {(1/Time.deltaTime).ToString("f0")}\n\n" +
                    $"Position: {transform.position.ToString("f4")}\n" +
                    $"HSpeed: {actualVelocity.x.ToString("f2")}\n" +
                    $"VSpeed: {actualVelocity.y.ToString("f2")}\n" +
                    $"Velocity: {actualVelocity.ToString("f2")}\n" +
                    $"WishVel: {wishVelocity.ToString("f2")}\n" +
                    $"Grounded: {controller.isGrounded()}"
        ;
    }
}
