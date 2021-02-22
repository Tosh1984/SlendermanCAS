using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalSlendermanEffects : MonoBehaviour
{
    [SerializeField] private AudioClip audioNear;
    [SerializeField] private AudioClip audioStatic;

    private float timeLooking = 0f;
    private bool isFirstTime = true;
    private bool isPlayerFlashlightOn;
    private float noticedSlenderAngle = 15f;
    private float maxTimeGazingSlender;

    Animator anim;
    private ExperimentalPlayerController playerController;
    private Transform mainCamera;
    private Transform player;
    private Transform slenderman;
    private AudioSource playerAudioSource;

    private void OnEnable() {
        ExperimentalGameEventManager.onFlashlightToggled += UpdateFlashlight;
    }

    private void Start()
    {
        ExperimentalGameEventHandler gameManager = ExperimentalGameEventHandler.Instance;

        playerController = gameManager.player;
        player = playerController.transform;
        slenderman = gameManager.slenderman.transform;
        maxTimeGazingSlender = gameManager.maxTimeGazingSlender;
        mainCamera = Camera.main.transform;
        anim = mainCamera.GetComponent<Animator>();
        playerAudioSource = playerController.GetComponent<AudioSource>();

        GetComponent<AudioSource>().clip = audioStatic;

        isPlayerFlashlightOn = playerController.GetComponentInChildren<ExperimentalFlashlightAnimation>().isFlashlightOn;
    }

    private void Update()
    {
        float distance = Vector3.Distance(slenderman.position, player.position);
        float angle = Vector3.Angle(mainCamera.forward, slenderman.position - player.position);

        if (distance < playerController.viewDistance && angle < noticedSlenderAngle) {
            timeLooking += Time.deltaTime;

            anim.SetBool("isLooked", true);

            Shock();
        } else {
            timeLooking = 0;

            anim.SetBool("isLooked", false);
        }

        if (timeLooking > maxTimeGazingSlender) {
            // lose
        }
    }

    private void OnDisable() {
        ExperimentalGameEventManager.onFlashlightToggled -= UpdateFlashlight;
    }

    private void Shock() {
        if (isFirstTime && isPlayerFlashlightOn) {
            playerAudioSource.PlayOneShot(audioNear, 1f);
            isFirstTime = false;
        }
    }

    private void UpdateFlashlight() {
        isPlayerFlashlightOn = !isPlayerFlashlightOn;
    }
}
