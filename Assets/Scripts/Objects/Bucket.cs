using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bucket : PlayerInteractible {

    [SerializeField] Transform ToPosition;
    [SerializeField] float dropSpeed = 0.3f;
    public bool isMoved = false;
    public override void InteractWithObject () {
        transform.DOMoveX(ToPosition.position.x, dropSpeed).SetEase(Ease.Linear);
        transform.DOMoveY(ToPosition.position.y, dropSpeed).SetEase(Ease.InQuad);
        isMoved = true;
    }

    void BucketFall () {
        GetComponent<Rigidbody>().useGravity = true;
        isMoved = false;
    }

}
