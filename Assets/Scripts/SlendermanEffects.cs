using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles and plays the glitch effect with audio when gazing slenderman.
/// Subscriptions:
/// - onPlayerViewEntered
/// - onNotPlayerViewEntered
/// </summary>
public class SlendermanEffects : MonoBehaviour
{
    [SerializeField] private AudioClip audioNear;
    [SerializeField] private AudioClip audioStatic;

    Animator anim;
    private PlayerController playerController;
    private Transform mainCamera;
    private AudioSource playerAudioSource;
    private GameEventHandler gameManager;

    private void OnEnable() {
        GameEventManager.onPlayerViewEntered += SlendermanGazed;
        GameEventManager.onNotPlayerViewEntered += SlendermanNotGazed;
        GameEventManager.onGameWon += Unglitch;
        GameEventManager.onGameLost += Unglitch;
    }

    private void Start()
    {
        gameManager = GameEventHandler.Instance;

        playerController = gameManager.player;

        mainCamera = Camera.main.transform;
        anim = mainCamera.GetComponent<Animator>();
        playerAudioSource = playerController.GetComponent<AudioSource>();

        GetComponent<AudioSource>().clip = audioStatic;
    }

    private void Update() {

    }

    private void OnDisable() {
        GameEventManager.onPlayerViewEntered -= SlendermanGazed;
        GameEventManager.onNotPlayerViewEntered -= SlendermanNotGazed;
        GameEventManager.onGameWon -= Unglitch;
        GameEventManager.onGameLost -= Unglitch;
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

    private void Unglitch() {
        anim.SetTrigger("GameEnd");
    }
}
