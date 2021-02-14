using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalRotateCube : MonoBehaviour {

    public float spinForce;
    public bool isSpinning = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (isSpinning) {
            transform.Rotate(0, spinForce * Time.deltaTime, 0);
        }
    }

    public void ToggleSpin() {
        isSpinning = !isSpinning;
    }

    public void ChangeSpin() {
        spinForce = -spinForce;
    }
}
