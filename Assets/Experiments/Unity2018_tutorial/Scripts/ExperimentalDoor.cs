using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Experimental;

namespace Experimental {

    public class ExperimentalDoor : MonoBehaviour
    {
        public Transform door;

        private void Start() {
            door = GetComponent<Transform>();
        }

        public void Hovering(bool enable) {
        
        }

        public void Selecting(bool enable) {
            if (enable) {
                door.transform.position += new Vector3(1f, 1f, 1f);
            }
        }
    }
}