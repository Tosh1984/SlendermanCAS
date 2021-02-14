using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ExperimentalReticleAnimation : MonoBehaviour {
    Animator anim;
    XRCardboardController cardboardController;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();

        cardboardController = GetComponentInParent<XRCardboardController>();
    }

    // Update is called once per frame
    void Update() {
        UpdateInteraction();
    }

    private void UpdateInteraction() {
        if (cardboardController.IsInteractableDetected()) {
            if (cardboardController.IsTriggerPressed()) {
                anim.SetBool("isActivated", false);
            } else {
                anim.SetBool("isActivated", true);
            }
        } else {
            anim.SetBool("isActivated", false);
        }
    }
}
