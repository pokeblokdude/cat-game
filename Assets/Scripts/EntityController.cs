using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EntityController : MonoBehaviour {
    
    [SerializeField] EntityPhysicsData entityPhysicsData;
    [SerializeField] LayerMask collisionMask;
    Rigidbody rb;
    CapsuleCollider col;

    int facing = 1;
    int touchingWall = 0;
    bool grounded;
    bool landedThisFrame = false;

    void Awake() {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update() {
        bool grnd = Physics.Raycast(transform.position + col.center, Vector3.down, col.height / 2 + 0.1f, collisionMask);
        if(grnd && !grounded) {
            grounded = true;
            landedThisFrame = true;
        }
        else if(grnd && grounded) {
            landedThisFrame = false;
        }
        else {
            grounded = false;
        }
        rb.drag = grounded ? entityPhysicsData.groundDrag : entityPhysicsData.airDrag;

        bool wall = Physics.Raycast(transform.position + col.center, Vector3.right * facing, col.radius + 0.1f, collisionMask);
        Debug.DrawRay(transform.position + col.center, Vector3.right * col.radius * facing, Color.red, Time.deltaTime);
        touchingWall = (wall ? 1 : 0) * facing;
    }

    public Vector3 Move(Vector3 moveAmount) {
        if(grounded) {
            rb.AddForce(moveAmount, ForceMode.Impulse);
        }
        else {
            rb.AddForce(moveAmount * entityPhysicsData.airMoveSpeedMultiplier, ForceMode.Impulse);
            if(rb.velocity == Vector3.zero) {
                rb.AddForce(Vector3.down * 0.1f, ForceMode.Impulse);
            }
        }
        return rb.velocity;
    }

    public Vector3 Jump(bool charged) {
        if(charged) {
            rb.AddForce(new Vector3(0.4f * facing, 1, 0).normalized * entityPhysicsData.chargedJumpForce, ForceMode.Impulse);
        }
        else {
            rb.AddForce(Vector3.up * entityPhysicsData.jumpForce, ForceMode.Impulse);
        }
        return rb.velocity;
    }

    public void SetFacing(int dir) {
        facing = dir;
    }

    public bool IsGrounded() {
        return grounded;
    }

    public bool LandedThisFrame() {
        return landedThisFrame;
    }

    public int TouchingWall() {
        return touchingWall;
    }
}
