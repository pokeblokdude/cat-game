using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionUI : MonoBehaviour {

    CanvasGroup group;

    void Start() {
        group = GetComponent<CanvasGroup>();
        group.DOFade(0, 1);
    }

    public void Play() {
        group.DOFade(1, 1);
    }
}
