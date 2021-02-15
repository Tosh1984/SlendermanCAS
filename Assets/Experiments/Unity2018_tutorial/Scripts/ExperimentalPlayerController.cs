using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ExperimentalPlayerController : MonoBehaviour
{
    public int collectedPages = 0;
    public int pagesToCollect = 8;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float viewDistance = 10f;
    public float reachDistance = 10f;
    [SerializeField] private float gravity = 9.8f;
    public bool isFlashlightOn = false;

    private CharacterController controller;
    private XRCardboardController cardboardController;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cardboardController = XRCardboardController.Instance;
        cardboardController.maxInteractionDistance = reachDistance;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerInteraction();
    }

    private void PlayerMovement() {
#if UNITY_EDITOR
        Camera mainCam = Camera.main;
        float maxAngle = 60f;
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

    private void PlayerInteraction() {
        // Interact
        if (cardboardController.IsTriggerPressed()) {
            // Collectables
            if (cardboardController.GetCollectable()) {
                collectedPages += 1;
            }
        }

        // Flashlight
        if (IsFlashlightTriggered()) {
            isFlashlightOn = !isFlashlightOn;
        }
    }

    private bool IsRunning() {
        return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.Space)) &&
            (Input.GetButton("Horizontal") || Input.GetButton("Vertical"));
    }

    public bool IsFlashlightTriggered() {
        return Input.GetKeyDown(KeyCode.F);
    }
}
