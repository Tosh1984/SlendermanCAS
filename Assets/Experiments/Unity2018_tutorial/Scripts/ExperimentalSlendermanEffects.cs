using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Experimental;

namespace Experimental {

    public class ExperimentalSlendermanEffects : MonoBehaviour {
        [SerializeField] private AudioClip audioNear;
        [SerializeField] private AudioClip audioStatic;

        Animator anim;
        private ExperimentalPlayerController playerController;
        private Transform mainCamera;
        private AudioSource playerAudioSource;

        private void OnEnable() {
            ExperimentalGameEventManager.onPlayerViewEntered += SlendermanGazed;
            ExperimentalGameEventManager.onNotPlayerViewEntered += SlendermanNotGazed;
        }

        private void Start() {
            ExperimentalGameEventHandler gameManager = ExperimentalGameEventHandler.Instance;

            playerController = gameManager.player;

            mainCamera = Camera.main.transform;
            anim = mainCamera.GetComponent<Animator>();
            playerAudioSource = playerController.GetComponent<AudioSource>();

            GetComponent<AudioSource>().clip = audioStatic;
        }

        private void Update() { }

        private void OnDisable() {
            ExperimentalGameEventManager.onPlayerViewEntered -= SlendermanGazed;
            ExperimentalGameEventManager.onNotPlayerViewEntered -= SlendermanNotGazed;
        }

        private void SlendermanGazed() {

            if (!anim.GetBool("isLooked")) {
                Shock();
            }
            anim.SetBool("isLooked", true);
        }

        private void SlendermanNotGazed() {
            anim.SetBool("isLooked", false);
        }

        private void Shock() {
            if (Random.value > 0.5f) {
                playerAudioSource.PlayOneShot(audioNear, 1f);
            }
        }
    }
}