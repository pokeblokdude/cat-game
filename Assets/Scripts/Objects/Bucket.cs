using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bucket : PlayerInteractible {

    [SerializeField] Transform ToPosition;

    public override void InteractWithObject () {
        DOTween.Sequence().Append(transform.DOMoveX(ToPosition.position.x, 0.6f).SetEase(Ease.InQuad))
                          .Append(transform.DOMoveY(ToPosition.position.y, 0.6f).SetEase(Ease.InQuad));
    }

}
