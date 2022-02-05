using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(EntityController))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] Transform sprite;
    [SerializeField] EntityPhysicsData playerData;
    PlayerInput input;
    EntityController controller;
    Rigidbody rb;
    
    Vector3 wishVelocity;
    Vector3 actualVelocity;

    bool jumping = false;
    float jumpStartTime;

    void Awake() {
        input = GetComponent<PlayerInput>();
        controller = GetComponent<EntityController>();
        rb = GetComponent<Rigidbody>();

        wishVelocity = Vector3.zero;
        actualVelocity = Vector3.zero;

        sprite.rotation = Quaternion.Euler(0, 180, 0);
        PlayerPrefs.SetInt("Progress", 0);  // initialize player progress to zero
        PlayerPrefs.Save();
    }

    void Update() {
        wishVelocity = new Vector3(input.moveDir * playerData.moveSpeed, 0, 0);

        if(Time.time - jumpStartTime > playerData.jumpTime || !input.jump) {
            jumping = false;
        }

        if(jumping && Time.time - jumpStartTime < playerData.jumpTime) {
            rb.useGravity = false;
        }
        else {
            rb.useGravity = true;
        }

        SetDebugText();
    }

    void FixedUpdate() {
        if(controller.isGrounded() && input.jump && !jumping) {
            controller.Jump();
            jumping = true;
            jumpStartTime = Time.time;
        }
        if(Mathf.Sign(wishVelocity.x) == 1 && wishVelocity.x != 0 && sprite.rotation.eulerAngles.y == 0) {
            print("flip right");
            FlipSprite();
        }
        else if(Mathf.Sign(wishVelocity.x) == -1 && Mathf.Abs(sprite.rotation.eulerAngles.y) == 180) {
            print("flip left");
            FlipSprite();
        }
        actualVelocity = controller.Move(wishVelocity);
    }

    void FlipSprite() {
        if(sprite.rotation.y == 0)
            sprite.DORotate(new Vector3(0, 180, 0), 0.2f);
        else
            sprite.DORotate(new Vector3(0, 0, 0), 0.2f);
    }

    void SetDebugText() {
        text.text = $"FPS: {(1/Time.deltaTime).ToString("f0")}\n" +
                    $"deltaTime: {Time.deltaTime.ToString("f8")}\n\n" +
                    $"Position: {transform.position.ToString("f4")}\n" +
                    $"HSpeed: {actualVelocity.x.ToString("f2")}\n" +
                    $"VSpeed: {actualVelocity.y.ToString("f2")}\n" +
                    $"Velocity: {actualVelocity.ToString("f2")}\n" +
                    $"WishVel: {wishVelocity.ToString("f3")}\n" +
                    $"Grounded: {controller.isGrounded()}\n" +
                    $"Jumping: {jumping}\n" +
                    $"progress: {PlayerPrefs.GetInt("Progress")}"
        ;
    }
}
