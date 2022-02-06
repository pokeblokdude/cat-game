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
        UpdateSprite();
        if (pen.GetComponent<Bucket>().isMoved) {
            anim.SetBool("clogged", true);
            print("asdfasdfasdfasdfasd");
        }
        // pen.GetComponent<Bucket>().isMoved
    }
}
