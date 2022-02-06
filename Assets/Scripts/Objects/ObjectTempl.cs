using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTempl : MonoBehaviour
{
    public GameObject P;    // get a reference to the player
    public int MaxUses = 100; // set the default max number of uses for this object
    public int ProgressRequirement;
    public AudioClip SoundEffect;

    internal AudioSource audioSource;

    //set up private variables for logic and messing about
    private int useCount = 0;
    private bool isUsing = false;
    private bool canInteract = false;

    //function run when player interacts with this object
    public virtual void InteractWithObject()
    {
        
    }



    // allow interactions if player is overlapping this object
    void OnTriggerEnter(Collider other)
    {
        if (other.name == P.name)
        {
            canInteract = true;
            print("enter");
        }
        
    }


    // disable interactions if the player leaves this object
    void OnTriggerExit(Collider other)
    {
        if (other.name == P.name)
        {
            canInteract = false;
            print("leave");
        }
    }


    // find the player object and set the reference variable
    void Start()
    {
        P = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {

        
        // if the player is trying to interact and 
        // we are bellow the max uses and 
        // we are not already using something and 
        // the player is overlapping us, run the folowing code

        if (P.GetComponent<PlayerInput>().interact && ( useCount < MaxUses ) && !isUsing && canInteract)
        {
            useCount ++;    // increase the amount of uses
            isUsing = true; // player is interaction with us

            InteractWithObject();
            print("test");


            // update player progress if at the correct progression for this
            if (PlayerPrefs.GetInt("Progress") == ProgressRequirement)
            {
                PlayerPrefs.SetInt("Progress", PlayerPrefs.GetInt("Progress") + 1);
            }
        }

        // stop interaction when the player releases the interaction key
        if (!P.GetComponent<PlayerInput>().interact)
        {
            isUsing = false;
        }
    }
}