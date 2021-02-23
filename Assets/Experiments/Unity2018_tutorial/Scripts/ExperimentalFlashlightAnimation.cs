using UnityEngine;
using System.Collections;
using Experimental;

namespace Experimental {

    public class ExperimentalFlashlightAnimation : MonoBehaviour {
        Animator anim;

        [Range(0.0f, 1.0f)]
        public float clickVolume = 0.7f;
        public bool isFlashlightOn = false;

        [SerializeField] private AudioClip clickOn;
        [SerializeField] private AudioClip clickOff;
        AudioSource audioSource;
        private Transform whiteLight;

        private void OnEnable() {
            ExperimentalGameEventManager.onFlashlightToggled += ToggleFlashlight;
            ExperimentalGameEventManager.onGamePaused += OnPaused;
            ExperimentalGameEventManager.onGameWon += OnGameResult;
            ExperimentalGameEventManager.onGameLost += OnGameResult;
        }

        private void Start() {
            anim = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();

            GetComponentInChildren<Light>().range = ExperimentalGameEventHandler.Instance.player.viewDistance;
            whiteLight = GameObject.Find("WhiteLight").transform;

            if (isFlashlightOn) {
                anim.SetBool("isOn", true);
            }
        }

        private void OnDisable() {
            ExperimentalGameEventManager.onFlashlightToggled -= ToggleFlashlight;
            ExperimentalGameEventManager.onGamePaused -= OnPaused;
            ExperimentalGameEventManager.onGameWon -= OnGameResult;
            ExperimentalGameEventManager.onGameLost -= OnGameResult;
        }

        private void ToggleFlashlight() {
            isFlashlightOn = !isFlashlightOn;

            if (isFlashlightOn) {
                anim.SetBool("isOn", true);
                audioSource.PlayOneShot(clickOn, clickVolume);
            } else {
                anim.SetBool("isOn", false);
                audioSource.PlayOneShot(clickOff, clickVolume);
            }
        }

        private void OnPaused() {
            whiteLight.gameObject.SetActive(!ExperimentalPauseAndShowMenu.Instance.isPaused);
        }

        private void OnGameResult() {
            whiteLight.gameObject.SetActive(false);
        }
    }
}