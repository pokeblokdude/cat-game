using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EntityController))]
[RequireComponent(typeof(PersonFSM))]
public class Person : MonoBehaviour {

    [SerializeField] EntityPhysicsData playerData;
    PersonFSM stateMachine;
    EntityController controller;
    
    Vector3 wishVelocity;
    Vector3 actualVelocity;

    void Awake() {
        stateMachine = GetComponent<PersonFSM>();
        controller = GetComponent<EntityController>();

        wishVelocity = Vector3.zero;
        actualVelocity = Vector3.zero;
    }

    void Update() {
        wishVelocity = new Vector3(stateMachine.moveDir * playerData.moveSpeed, 0, 0);

    }

    void FixedUpdate() {
        actualVelocity = controller.Move(wishVelocity);
    }

}
