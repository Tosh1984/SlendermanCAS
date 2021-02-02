using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int userSpeed;

    private float horizontalInput;
    private float verticalInput;
    private float jumpInput;
    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start() {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");
    }

    private void FixedUpdate() {
        rigidbodyComponent.velocity = new Vector3(horizontalInput * userSpeed, 0, verticalInput * userSpeed);
    }
}
