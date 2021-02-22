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

    private void Start() {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        GetComponentInChildren<Light>().range = ExperimentalGameEventHandler.Instance.player.viewDistance;

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
