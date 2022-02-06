using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bucket : ObjectTempl
{

    [SerializeField] Transform ToPosition;
    public override void InteractWithObject ()
    {
        DOTween.Sequence().Append(transform.DOMoveX(ToPosition.position.x, 0.6f))
            .Append(transform.DOMoveY(ToPosition.position.y, 0.6f));
    }
}
