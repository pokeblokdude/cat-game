using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionUI : MonoBehaviour {

    CanvasGroup group;

    void Awake() {
        group = GetComponent<CanvasGroup>();
    }

    void Start() {
        group.alpha = 1;
    }

    public void Play() {
        group.DOFade(1, 1).SetUpdate(true);
    }

    public void PlayEnter() {
        print("play enter fade");
        group.DOFade(0, 1).SetUpdate(true);
    }
}
