using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionUI : MonoBehaviour {

    CanvasGroup group;

    void Awake() {
        group = GetComponent<CanvasGroup>();
        group.alpha = 1;
    }

    void Start() {
        group.DOFade(0, 1).SetUpdate(true);
    }

    public void Play() {
        group.DOFade(1, 1).SetUpdate(true);
    }
}
