using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : PlayerInteractible
{

    public override void InteractWithObject ()
    {
        GetComponent<Rigidbody>().useGravity = true;
        UpdateSprite();
    }
}
