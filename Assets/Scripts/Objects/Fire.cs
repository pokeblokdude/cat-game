using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : PlayerInteractible
{

    Animator anim;
    void Awake () {
        anim = GetComponent<Animator>();
    }
    public override void InteractWithObject ()
    {
        UpdateSprite();
        StartCoroutine(Wait());
        


   }

    IEnumerator Wait() {
        anim.SetBool("finish", true);
        yield return new WaitForSeconds(10);
        if ( tag == "NeedsFixing") {
            yield return new WaitForSeconds(3);
            PlayerPrefs.SetInt("Progress", PlayerPrefs.GetInt("Progress") + 1);
        }
    }
}
