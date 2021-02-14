using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ExperimentalPlayerController : MonoBehaviour
{
    public int collectedPages = 0;
    public int pagesToCollect = 8;
    public float playerSpeed = 5f;
    public float viewDistance = 10f;
    public float reachDistance = 10f;
    [SerializeField] private float gravity = 9.8f;

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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * playerSpeed;
        velocity.y -= gravity;
        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayerInteraction() {
        if (cardboardController.IsTriggerPressed()) {

            // Collectables
            if (cardboardController.GetCollectable()) {
                collectedPages += 1;
            }

        }
    }
}
