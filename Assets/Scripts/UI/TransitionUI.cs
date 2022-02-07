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

    public void Play() {
        print("playing fade");
        group.DOFade(1, 1).SetUpdate(true);
    }

    public void PlayEnter() {
        group.DOFade(0, 1).SetUpdate(true);
    }
}
