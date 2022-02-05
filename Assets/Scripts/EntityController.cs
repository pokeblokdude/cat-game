using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EntityController : MonoBehaviour {
    
    [SerializeField] EntityPhysicsData entityPhysicsData;
    [SerializeField] LayerMask collisionMask;
    Rigidbody rb;
    CapsuleCollider col;

    bool grounded;

    void Awake() {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update() {
        grounded = Physics.Raycast(transform.position + col.center, Vector3.down, col.height / 2 + 0.1f, collisionMask);
        rb.drag = grounded ? entityPhysicsData.groundDrag : entityPhysicsData.airDrag;
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
        rb.AddForce(Vector3.up * (charged ? entityPhysicsData.chargedJumpForce : entityPhysicsData.jumpForce), ForceMode.Impulse);
        return rb.velocity;
    }


    public bool isGrounded() {
        return grounded;
    }

}
