using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour {

    Image image;

    [SerializeField] Sprite imageNotHovered;
    [SerializeField] Sprite imageHovered;

    void Start() {
        image = GetComponent<Image>();
    }

    public void HoverEnter() {
        image.sprite = imageHovered;
    }

    public void HoverExit() {
        image.sprite = imageNotHovered;
    }
}
