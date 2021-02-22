using UnityEngine;
using System.Collections;

public class ExperimentalFlashlightAnimation : MonoBehaviour
{
    Animator anim;

    [Range(0.0f, 1.0f)]
    public float clickVolume = 0.7f;
    public bool isFlashlightOn = false;

    [SerializeField] private AudioClip clickOn;
    [SerializeField] private AudioClip clickOff;
    AudioSource audioSource;

    private void OnEnable() {
        ExperimentalGameEventManager.onFlashlightToggled += ToggleFlashlight;
    }

    // Start is called before the first frame update
    private void Start() {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (isFlashlightOn) {
            anim.SetBool("isOn", true);
        }
    }

    private void OnDisable() {
        ExperimentalGameEventManager.onFlashlightToggled -= ToggleFlashlight;
    }

    private void ToggleFlashlight() {
        isFlashlightOn = !isFlashlightOn;

        if (isFlashlightOn) {
            anim.SetBool("isOn", true);
            audioSource.PlayOneShot(clickOn, clickVolume);
        } else {
            anim.SetBool("isOn", false);
            audioSource.PlayOneShot(clickOff, clickVolume);
        }
    }
}
