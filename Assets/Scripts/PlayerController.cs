﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float allowedRunningSeconds = 60f;
    public float runCooldown = 10f;
    public float viewDistance = 10f;
    public float viewAngle = 25f;
    public float reachDistance = 2.5f;

    public bool doesMovementFollowCamera = false;
    [SerializeField] private float gravity = 9.8f;

    private Camera mainCamera;
    private CharacterController controller;
    private XRCardboardController cardboardController;
    private GameEventHandler gameManager;

    private float tempAllowedRunningSeconds;
    private float elapsed = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
        cardboardController = XRCardboardController.Instance;
        cardboardController.maxInteractionDistance = reachDistance;
        gameManager = GameEventHandler.Instance;

        tempAllowedRunningSeconds = allowedRunningSeconds;
    }

    private void Update()
    {

        // todo: disable pausing when game finished


        // EVENT: onGamePaused
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseAndShowMenu.Instance.Pause();
        }

        PlayerMovement();

        if (PauseAndShowMenu.Instance.isPaused) { return; }

        // MARK: Pauseable actions below
        CheckGazeOnSlenderman();

        if (cardboardController.IsGettingCollectable()) {
            PlayerCollectPage();
        }

        // EVENT: onFlashlightToggled
        if (Input.GetKeyDown(KeyCode.F)) {
            GameEventManager.InvokeFlashlightToggled();
        }
    }

    private void PlayerMovement() {
#if UNITY_EDITOR
        float maxAngle = 85f;
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        
        mainCamera.transform.localRotation = Quaternion.Euler(
            new Vector4(
                Mathf.Clamp(-1f * (mouseY * 180f), -maxAngle, maxAngle),
                Mathf.Clamp(mouseX * 360f, -maxAngle, maxAngle),
                transform.localRotation.z
                )
            );
#endif

        float horizontalNorm = Input.GetAxis("Horizontal");
        float verticalNorm = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalNorm, 0, verticalNorm);
        if (doesMovementFollowCamera) {
            direction.x = verticalNorm * mainCamera.transform.forward.x +
                          horizontalNorm * mainCamera.transform.right.x;
            direction.z = verticalNorm * mainCamera.transform.forward.z +
                          horizontalNorm * mainCamera.transform.right.z;
        }
        Vector3 velocity = direction * (IsRunning() ? runSpeed : walkSpeed);
        velocity.y -= gravity;
        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayerCollectPage() {
        // EVENT: onGettingCollectable
        GameEventManager.InvokeGettingCollectable();
        cardboardController.GetCollectable();

        // EVENT: onGotCollectable
        GameEventManager.InvokeGotCollectable();
    }

    private bool IsRunning() {
        if (allowedRunningSeconds <= 0) {

            elapsed += Time.deltaTime;
            if (elapsed >= runCooldown) {
                allowedRunningSeconds = tempAllowedRunningSeconds;
                elapsed = 0f;
            }

            return false;
        } else if ((Input.GetKey(KeyCode.LeftShift) ||
            Input.GetKey(KeyCode.RightShift)) &&
                (Input.GetButton("Horizontal") ||
                Input.GetButton("Vertical"))) {
            allowedRunningSeconds -= Time.deltaTime;
            return true;
        } else {
            return false;
        }
    }

    private void CheckGazeOnSlenderman() {
        if (gameManager.slenderman == null) { return; }

        Transform slenderman = gameManager.slenderman.transform;
        Transform player = transform;

        float distance = Vector3.Distance(slenderman.position, player.position);
        float angle = Vector3.Angle(mainCamera.transform.forward, slenderman.position - player.position);

        if (distance < viewDistance && angle < viewAngle) {
            RaycastHit hit;
            if (Physics.Raycast(player.position,
                                slenderman.position - player.position,
                                out hit,
                                viewDistance)) {
                if (hit.transform.name == slenderman.name) {
                    // EVENT: onPlayerViewEntered
                    GameEventManager.InvokePlayerViewEntered();
                }
            }
        } else {
            // EVENT: onNotPlayerViewEntered
            GameEventManager.InvokeNotPlayerViewEntered();
        }
    }
}
