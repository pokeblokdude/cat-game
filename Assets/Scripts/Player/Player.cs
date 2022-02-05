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
    Animator anim;
    
    Vector3 wishVelocity;
    Vector3 actualVelocity;

    bool jumping = false;
    float jumpStartTime;
    bool holdingJump = false;

    bool chargedJumping = false;
    bool crouching = false;
    float crouchStartTime;

    void Awake() {
        input = GetComponent<PlayerInput>();
        controller = GetComponent<EntityController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        wishVelocity = Vector3.zero;
        actualVelocity = Vector3.zero;

        sprite.rotation = Quaternion.Euler(0, 180, 0);
        PlayerPrefs.SetInt("Progress", 0);  // initialize player progress to zero
        PlayerPrefs.Save();
    }

    void Update() {
        // logic for not letting you hold down jump to keep jumping
        if(controller.LandedThisFrame() && input.jump) {
            holdingJump = true;
        }
        if(!input.jump) {
            holdingJump = false;
        }

        wishVelocity = new Vector3(input.moveDir * playerData.moveSpeed, 0, 0);

        // variable height jump
        if(Time.time - jumpStartTime > playerData.jumpTime || !input.jump) {
            jumping = false;
        }
        if(jumping && Time.time - jumpStartTime < playerData.jumpTime) {
            rb.useGravity = false;
            anim.SetBool("grounded", false);
        }
        else {
            rb.useGravity = true;
        }

        if(input.crouch) {
            anim.SetBool("crouch", true);
            if(!crouching) {
                crouching = true;
                crouchStartTime = Time.time;
            }
        }
        else {
            anim.SetBool("crouch", false);
            crouching = false;
        }

        // setting variable for the animation controller
        anim.SetFloat("vSpeed", actualVelocity.y);
        anim.SetFloat("hSpeed", Mathf.Abs(actualVelocity.x));

        SetDebugText();
    }

    void FixedUpdate() {
        // jump if grounded and not already jumping
        if(controller.IsGrounded() && input.jump && !jumping) {
            if(input.crouch && Time.time - crouchStartTime > playerData.chargeTime) {
                controller.Jump(true);
                chargedJumping = true;
            }
            else {
                controller.Jump(false);
                jumpStartTime = Time.time;
            }
            jumping = true;
            anim.SetBool("jump", true);
            anim.SetBool("grounded", false);
        }

        // play grounded animation, unless we started jumping this frame
        if(controller.IsGrounded() && !jumping) {
            anim.SetBool("grounded", true);
            anim.SetBool("jump", false);
        }

        // play flipping animation if changing directions
        if(Mathf.Sign(wishVelocity.x) == 1 && wishVelocity.x != 0 && sprite.rotation.eulerAngles.y == 0) {
            print("flip right");
            FlipSprite();
            controller.SetFacing(1);
        }
        else if(Mathf.Sign(wishVelocity.x) == -1 && Mathf.Abs(sprite.rotation.eulerAngles.y) == 180) {
            print("flip left");
            FlipSprite();
            controller.SetFacing(-1);
        }

        // apply movement to controller
        if(!input.crouch || !controller.IsGrounded()) {
            actualVelocity = controller.Move(wishVelocity);
        }
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
                    $"Grounded: {controller.IsGrounded()}\n" +
                    $"Jumping: {jumping}\n" +
                    $"Charged: {input.crouch &&  Time.time - crouchStartTime > playerData.chargeTime}\n\n" +
                    $"progress: {PlayerPrefs.GetInt("Progress")}"
        ;
    }
}
