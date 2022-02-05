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
        grounded = Physics.Raycast(transform.position, Vector3.down, col.height / 2 + 0.1f, collisionMask);
        rb.drag = grounded ? entityPhysicsData.groundDrag : entityPhysicsData.airDrag;
    }

    public Vector3 Move(Vector3 moveAmount) {
        if(grounded) {
            rb.AddForce(moveAmount, ForceMode.Impulse);
        }
        else {
            rb.AddForce(moveAmount * entityPhysicsData.airMoveSpeedMultiplier, ForceMode.Impulse);
        }
        return rb.velocity;
    }

    public Vector3 Jump() {
        rb.AddForce(Vector3.up * entityPhysicsData.jumpForce, ForceMode.Impulse);
        return rb.velocity;
    }


    public bool isGrounded() {
        return grounded;
    }

}
