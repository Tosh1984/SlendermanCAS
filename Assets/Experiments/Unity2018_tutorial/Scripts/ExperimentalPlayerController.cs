using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ExperimentalPlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float viewDistance = 10f;
    public float reachDistance = 2.5f;
    //public bool isFlashlightOn = false;
    [SerializeField] private float gravity = 9.8f;

    private CharacterController controller;
    private ExperimentalGameCardboardController cardboardController;

    //private void OnEnable() {
    //    ExperimentalGameEventManager.onGettingCollectable += PlayerCollectPage;
    //}

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cardboardController = ExperimentalGameCardboardController.Instance;
        cardboardController.maxInteractionDistance = reachDistance;
    }

    private void Update()
    {
        PlayerMovement();
        PlayerCollectPage();
    }

    //private void OnDisable() {
    //    ExperimentalGameEventManager.onGettingCollectable -= PlayerCollectPage;
    //}

    private void PlayerMovement() {
#if UNITY_EDITOR
        Camera mainCam = Camera.main;
        float maxAngle = 85f;
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        
        mainCam.transform.localRotation = Quaternion.Euler(
            new Vector4(
                Mathf.Clamp(-1f * (mouseY * 180f), -maxAngle, maxAngle),
                Mathf.Clamp(mouseX * 360f, -maxAngle, maxAngle),
                transform.localRotation.z
                )
            );
#endif

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity;
        velocity = direction * (IsRunning() ? runSpeed : walkSpeed);
        velocity.y -= gravity;
        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayerCollectPage() {
        if (cardboardController.IsGettingCollectable()) {
            ExperimentalGameEventHandler.Instance.pagesCollected += 1;
            cardboardController.GetCollectable();
        }
    }

    private bool IsRunning() {
        return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.Space)) &&
            (Input.GetButton("Horizontal") || Input.GetButton("Vertical"));
    }

    //private void IsFlashlightTriggered() {
    //    isFlashlightOn = !isFlashlightOn;
    //}
}
