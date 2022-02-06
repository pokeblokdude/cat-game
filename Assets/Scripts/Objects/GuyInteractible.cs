using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyInteractible : MonoBehaviour {
    [SerializeField] float interactionTime = 1;

    public float InteractionTime() {
        return interactionTime;
    }
}
