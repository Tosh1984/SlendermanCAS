using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReticleScript : MonoBehaviour
{
    Animator anim;

    public int distanceOfRay = 10;

    private RaycastHit hit;
    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, distanceOfRay)) {
            if (hit.transform.CompareTag("Selectable")) {
                anim.SetBool("isActivated", true);
            }
        } else {
            anim.SetBool("isActivated", false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            anim.SetBool("isActivated", false);
        }
    }
}
