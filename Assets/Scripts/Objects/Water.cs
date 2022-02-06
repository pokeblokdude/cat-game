using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : PlayerInteractible
{

    GameObject pen;
    Animator anim;
    void Awake () {
        pen = GameObject.Find("Pen");
        anim = GetComponent<Animator>();
    }
    public override void InteractWithObject ()
    {
        // if (pen.GetComponent<Bucket>().isMoved) {
        //     anim.SetBool()
        // }
        UpdateSprite();
        // pen.GetComponent<Bucket>().isMoved
    }
}
