using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTempl : MonoBehaviour
{
    public GameObject P;    // get a reference to the player
    public int MaxUses = 1; // set the default max number of uses for this object



    //set up private variables for logic and messing about
    private int useCount = 0;
    private Vector3 objScale;
    private bool isUsing = false;
    private bool canInteract = false;

    //function run when player interacts with this object
    void InteractWithObject()
    {

        objScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(objScale.x, objScale.y*2, objScale.z);

    }



    // allow interactions if player is overlapping this object
    void OnTriggerEnter()
    {
        canInteract = true;
        print("contact");
    }


    // disable interactions if the player leaves this object
    void OnTriggerExit()
    {
        canInteract = false;
        print("leave");
    }


    // find the player object and set the reference variable
    void Start()
    {
        P = GameObject.Find("Player");
    }



    void Update()
    {
        // if the player is trying to interact and 
        // we are bellow the max uses and 
        // we are not already using something and 
        // the player is overlapping us, run the folowing code

        if (P.GetComponent<PlayerInput>().interact && useCount < MaxUses && !isUsing && canInteract)
        {
            useCount ++;    // increase the amount of uses
            isUsing = true; // player is interaction with us

            InteractWithObject();

        }

        // stop interaction when the player releases the interaction key
        if (!P.GetComponent<PlayerInput>().interact)
        {
            isUsing = false;
        }
    }
}
