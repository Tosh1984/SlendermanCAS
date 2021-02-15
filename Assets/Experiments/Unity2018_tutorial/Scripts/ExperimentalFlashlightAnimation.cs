using UnityEngine;
using System.Collections;

public class ExperimentalFlashlightAnimation : MonoBehaviour
{
    Animator anim;
    ExperimentalPlayerController playerController;

    [Range(0.0f, 1.0f)]
    public float clickVolume = 0.7f;

    [SerializeField] private AudioClip clickOn;
    [SerializeField] private AudioClip clickOff;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponentInParent<ExperimentalPlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (playerController.IsFlashlightTriggered()) {
            if (playerController.isFlashlightOn) {
                anim.SetBool("isOn", true);
                audioSource.PlayOneShot(clickOn, clickVolume);
            } else {
                anim.SetBool("isOn", false);
                audioSource.PlayOneShot(clickOff, clickVolume);
            }
        }
    }
}
