using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTempl : MonoBehaviour
{
    public GameObject P;
    public int MaxUses = 1;


    private int useCount = 0;
    private Vector3 objScale;
    private bool isUsing = false;


    void InteractWithObject()
    {

        objScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(objScale.x, objScale.y*2, objScale.z);

    }







    void Start()
    {
        P = GameObject.Find("Player");
    }



    void Update()
    {
        if (P.GetComponent<PlayerInput>().interact && useCount < MaxUses && !isUsing)
        {
            useCount ++;
            isUsing = true;

            InteractWithObject();

        }

        if (!P.GetComponent<PlayerInput>().interact)
        {
            isUsing = false;
        }
    }
}
