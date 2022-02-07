using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsSequencer : MonoBehaviour {

    [SerializeField] Sprite[] sprites;
    Image image;

    void Start() {
        image = GetComponent<Image>();
    }

    public void StartSequence() {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence() {
        for(int i = 0; i < sprites.Length; i++) {
            image.sprite = sprites[i];
            if(i == 0) {
                yield return new WaitForSeconds(10);
            }
            else {
                yield return new WaitForSeconds(8);
            }
        }
        FindObjectOfType<CreditsManager>().LoadMenu();
    }
}
