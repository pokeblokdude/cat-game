using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuyInteractible : MonoBehaviour {
    [SerializeField] float interactionTime = 1;

    public float InteractionTime() {
        return interactionTime;
    }


    RuntimeAnimatorController animControler;
    Vector3 initialPosition;

    void Awake () {
        animControler = GetComponent<Animator>().runtimeAnimatorController;
        initialPosition = transform.position;
    }
    public void Reset () {
        tag = "Untagged";
        GetComponent<Animator>().runtimeAnimatorController = animControler;
        transform.DOMove(initialPosition, 1f).SetEase(Ease.Linear);
        if (GetComponent<Rigidbody> () != null) {
            GetComponent<Rigidbody>().useGravity = false;

        }
    }
}
