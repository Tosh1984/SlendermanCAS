﻿using UnityEngine;

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