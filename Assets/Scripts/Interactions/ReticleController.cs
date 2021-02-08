using UnityEngine;

namespace Interactions {
    [RequireComponent(typeof(Animator))]
    public class ReticleController : MonoBehaviour {
        private Animator animator;
        private static readonly int IsOpen = Animator.StringToHash("isOpen");
        private static readonly int Click = Animator.StringToHash("click");

        private void Start() {
            animator = GetComponent<Animator>();
        }

        public void ActivateReticle() {
            animator.SetBool(IsOpen, true);
        }

        public void DeactivateReticle() {
            animator.SetBool(IsOpen, false);
        }

        public void ClickReticle() {
            animator.SetTrigger(Click);
        }
    }
}