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
    [SerializeField] float angryTimeTotal = 10;
    [SerializeField] float angryStopDelay = 2;

    EntityController controller;
    Animator anim;
    Player player;
    GuyInteractible[] interactibles;

    Vector3 actualVelocity;
    ActionState actionState;
    GuyInteractible interactionTarget;

    bool catIsPettable = false;

    float stateEnterTime;
    bool enteredStateThisFrame = false;

    float timeToIdle;

    int walkDirection;
    float timeToWalk;

    bool overlappingCat;
    float overlapCatTimestamp;
    bool catOutOfRange;
    float catOutOfRangeTimestamp;
    

    void Awake() {
        controller = GetComponent<EntityController>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
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
                    timeToIdle = Random.Range(idleTimeRange.x, idleTimeRange.y);
                    print("time to idle: " + timeToIdle);
                }
                if(Time.time - stateEnterTime > timeToIdle) {
                    ChangeState(ActionState.AIMLESS_WALK);
                }
                for(int i = 0; i < interactibles.Length; i++) {
                    if(interactibles[i].CompareTag("NeedsFixing")) {
                        interactionTarget = interactibles[i];
                        ChangeState(ActionState.ALERTED);
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
                        ChangeState(ActionState.ALERTED);
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
            // ============================================================================ ALERTED
            case ActionState.ALERTED:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                    anim.SetBool("alerted", true);
                    walkDirection = (int)Mathf.Sign(interactionTarget.transform.position.x - transform.position.x);
                    Move(walkDirection);
                }
                // amount of time to be frozen/alerted for
                if(Time.time - stateEnterTime > 2) {
                    ChangeState(ActionState.NEEDS_TO_FIX_OBJECT);
                    anim.SetBool("alerted", false);
                }
                break;
            // ================================================================================= ANGRY
            case ActionState.ANGRY:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                    anim.SetBool("angry", true);
                }
                walkDirection = (int)Mathf.Sign(player.transform.position.x - transform.position.x);
                if(Mathf.Abs(transform.position.x - player.transform.position.x) > 0.3f) {
                    overlappingCat = false;
                    Move(walkDirection);
                }
                else {
                    if(!overlappingCat) {
                        overlappingCat = true;
                        overlapCatTimestamp = Time.time;
                    }
                    if(Time.time - overlapCatTimestamp > 0.1) {
                        anim.SetBool("angry", false);
                        ChangeState(ActionState.PICKING_UP_CAT);
                    }
                }
                // 2.5 = 1st shelf height (ish)
                if(player.transform.position.y > 2.5f) {
                    if(!catOutOfRange) {
                        catOutOfRange = true;
                        catOutOfRangeTimestamp = Time.time;
                    }
                }
                else {
                    catOutOfRange = false;
                }
                if((catOutOfRange && Time.time - catOutOfRangeTimestamp > angryStopDelay) || Time.time - stateEnterTime > angryTimeTotal) {
                    anim.SetBool("angry", false);
                    ChangeState(ActionState.IDLE);
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
                    interactionTarget.Reset();
                    if(interactionTarget.WillBonk()) {
                        ChangeState(ActionState.BONKED);
                    }
                    else {
                        ChangeState(ActionState.IDLE);
                    }
                    anim.SetBool("fixing", false);
                }
                break;
            // ============================================================================== BONKED
            case ActionState.BONKED:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                    anim.SetBool("bonked", true);
                }
                if(Time.time - stateEnterTime > 3) {
                    ChangeState(ActionState.ANGRY);
                    anim.SetBool("bonked", false);
                }
                break;
            // ============================================================================== PICKING_UP_CAT
            case ActionState.PICKING_UP_CAT:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                    anim.SetBool("fixing", true);
                }
                if(Time.time - stateEnterTime > 2) {
                    anim.SetBool("fixing", false);
                    ChangeState(ActionState.GAME_OVER_WALK);
                }
                break;
            // ============================================================================== GAME_OVER_WALK
            case ActionState.GAME_OVER_WALK:
                if(enteredStateThisFrame) {
                    enteredStateThisFrame = false;
                    walkDirection = -1;
                    FindObjectOfType<GameManager>().GameOver();
                }
                Move(walkDirection);
                break;
            // =============================================================================== ERROR
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
        ALERTED,
        ANGRY,
        NEEDS_TO_FIX_OBJECT,
        FIXING_OBJECT,
        BONKED,
        PICKING_UP_CAT,
        GAME_OVER_WALK
    }
}
