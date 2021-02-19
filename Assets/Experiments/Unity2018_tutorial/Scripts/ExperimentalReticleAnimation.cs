using UnityEngine;

public class ExperimentalReticleAnimation : MonoBehaviour {
    Animator anim;
    ExperimentalGameCardboardController cardboardController;

    private void Start() {
        anim = GetComponent<Animator>();
        cardboardController = ExperimentalGameCardboardController.Instance;
    }

    private void Update() {
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
