using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFSM : MonoBehaviour
{

    public int TimeBetweenStates = 500;
    public float moveDir { get; private set; }
    public enum PersonState
    {
        idle,
        moveRight,
        moveLeft,
        interact,
        pickupCat
    }

    private PersonState state = PersonState.idle;

    private int timeSinceState;

    void Start()
    {

    }
 
    void Update()
    {

        timeSinceState ++;
        print(timeSinceState);
        
        switch (state)
        {
            case PersonState.idle:
                moveDir = 0;
                if (timeSinceState > TimeBetweenStates)
                {
                    state = PersonState.moveRight;
                    timeSinceState = 0;
                }
                break;
            case PersonState.moveRight:
                moveDir = 1;
                if (timeSinceState > TimeBetweenStates)
                {
                    state = PersonState.moveLeft;
                    timeSinceState = 0;
                }
                break;
            case PersonState.moveLeft:
                moveDir = -1;
                if (timeSinceState > TimeBetweenStates)
                {
                    state = PersonState.idle;
                    timeSinceState = 0;
                }
                break;
            // case PersonState.interact:
            //     moveDir = 0;
            //     state = PersonState.idle;
            // case PersonState.pickupCat:
            //     moveDir = 0;
            //     state = PersonState.idle;

            default:
                break;

        }

    }
}
