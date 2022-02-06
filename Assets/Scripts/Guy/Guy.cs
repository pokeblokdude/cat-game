using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(EntityController))]
public class Guy : MonoBehaviour {
    
    [SerializeField] EntityPhysicsData guyData;
    [SerializeField] Transform sprite;
    [SerializeField] Vector2 idleTimeRange = new Vector2(0, 10);
    [SerializeField] Vector2 walkTimeRange = new Vector2(0, 10);

    EntityController controller;
    Animator anim;
    GuyInteractible[] interactibles;

    ActionState actionState;

    Vector3 actualVelocity;
    float stateEnterTime;
    bool enteredStateThisFrame = false;

    float timeToIdle;

    int walkDirection;
    float timeToWalk;

    GuyInteractible interactionTarget;

    void Awake() {
        controller = GetComponent<EntityController>();
        anim = GetComponent<Animator>();
        interactibles = FindObjectsOfType<GuyInteractible>();

        actionState = ActionState.IDLE;
    }

    void Start() {
        stateEnterTime = Time.time;
        enteredStateThisFrame = true;
        for(int i = 0; i < interactibles.Length; i++) {
            print(interactibles[i].name);
        }
    }

    void FixedUpdate() {
        switch(actionState) {
            // ====================================================================== IDLE
            case ActionState.IDLE:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                    anim.SetBool("fixing", false);
                    timeToIdle = Random.Range(idleTimeRange.x, idleTimeRange.y);
                    print("time to idle: " + timeToIdle);
                }
                if(Time.time - stateEnterTime > timeToIdle) {
                    ChangeState(ActionState.AIMLESS_WALK);
                }
                for(int i = 0; i < interactibles.Length; i++) {
                    if(interactibles[i].CompareTag("NeedsFixing")) {
                        interactionTarget = interactibles[i];
                        ChangeState(ActionState.NEEDS_TO_FIX_OBJECT);
                    }
                }
                Move(0);
                break;
            // ======================================================================= AIMLESS_WALK
            case ActionState.AIMLESS_WALK:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                    walkDirection = (int)Mathf.Sign(Random.Range(-1, 1));

                    timeToWalk = Random.Range(walkTimeRange.x, walkTimeRange.y);
                    print("time to walk: " + timeToWalk);
                }
                if(controller.TouchingWall() != 0) {
                    walkDirection = -walkDirection;
                }
                Move(walkDirection);

                if(Time.time - stateEnterTime > timeToWalk) {
                    ChangeState(ActionState.IDLE);
                }
                for(int i = 0; i < interactibles.Length; i++) {
                    if(interactibles[i].CompareTag("NeedsFixing")) {
                        interactionTarget = interactibles[i];
                        ChangeState(ActionState.NEEDS_TO_FIX_OBJECT);
                    }
                }

                break;
            // ======================================================================== WANTS_TO_PET_CAT
            case ActionState.WANTS_TO_PET_CAT:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                }
                break;
            // ============================================================================ PETTING_CAT
            case ActionState.PETTING_CAT:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                }
                break;
            // ================================================================================= ANGRY
            case ActionState.ANGRY:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                }
                break;
            // ======================================================================= NEEDS_TO_FIX_OBJECT
            case ActionState.NEEDS_TO_FIX_OBJECT:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                    print("need to fix");
                }
                walkDirection = (int)Mathf.Sign(interactionTarget.transform.position.x - transform.position.x);
                if(Mathf.Abs(transform.position.x - interactionTarget.transform.position.x) > 0.1f) {
                    Move(walkDirection);
                }
                else {
                    ChangeState(ActionState.FIXING_OBJECT);
                }
                break;
            // ========================================================================= FIXING_OBJECT
            case ActionState.FIXING_OBJECT:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                    anim.SetBool("fixing", true);
                    print("fixing");
                }
                if(Time.time - stateEnterTime > interactionTarget.InteractionTime()) {
                    interactionTarget.tag = "Untagged";
                    ChangeState(ActionState.IDLE);
                }
                break;
            // ========================================================================= BUCKET_ON_HEAD
            case ActionState.BUCKET_ON_HEAD:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                }
                break;
            // ========================================================================= ERROR
            default:
                throw new System.Exception("Guy entered unkown state");
        }
        
        // set animation variables
        anim.SetFloat("hSpeed", Mathf.Abs(actualVelocity.x));
    }

    void ChangeState(ActionState state) {
        actionState = state;
        stateEnterTime = Time.time;
        enteredStateThisFrame = true;
    }

    void Move(int dir) {
        // play flipping animation if changing directions
        if(dir == 1 && Mathf.Abs(sprite.rotation.eulerAngles.y) == 180) {
            FlipSprite();
            controller.SetFacing(1);
        }
        else if(dir == -1 && Mathf.Abs(sprite.rotation.eulerAngles.y) == 0) {
            FlipSprite();
            controller.SetFacing(-1);
        }
        actualVelocity = controller.Move(new Vector3(dir * guyData.moveSpeed, 0, 0));
    }

    void FlipSprite() {
        if(sprite.rotation.y == 0)
            sprite.DORotate(new Vector3(0, 180, 0), 0.2f);
        else
            sprite.DORotate(new Vector3(0, 0, 0), 0.2f);
    }

    enum ActionState {
        IDLE,
        AIMLESS_WALK,
        WANTS_TO_PET_CAT,
        PETTING_CAT,
        ANGRY,
        NEEDS_TO_FIX_OBJECT,
        FIXING_OBJECT,
        // unique states
        BUCKET_ON_HEAD
    }
}
