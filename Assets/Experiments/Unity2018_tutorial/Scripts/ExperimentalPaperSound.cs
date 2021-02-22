using System.Collections.Generic;
using UnityEngine;

// todo: rename to ExperimentalGrabPaperSound
public class ExperimentalPaperSound : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float grabVolume = 1f;

    [SerializeField] private AudioClip audio1;
    [SerializeField] private AudioClip audio2;

    private List<AudioClip> audioArray;
    private AudioSource audioSource;
    System.Random rnd = new System.Random();

    private void OnEnable() {
        ExperimentalGameEventManager.onGotCollectable += PlaySoundEffect;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        audioArray = new List<AudioClip> {
            audio1,
            audio2
        };
    }

    private void OnDisable() {
        ExperimentalGameEventManager.onGotCollectable -= PlaySoundEffect;
    }

    public void PlaySoundEffect() {
        int r = rnd.Next(audioArray.Count);
        audioSource.PlayOneShot(audioArray[r], grabVolume);
    }
}
