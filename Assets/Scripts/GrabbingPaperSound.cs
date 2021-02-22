using System.Collections.Generic;
using UnityEngine;

public class GrabbingPaperSound : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float grabVolume = 1f;

    [SerializeField] private AudioClip audio1;
    [SerializeField] private AudioClip audio2;

    private List<AudioClip> audioArray;
    private AudioSource audioSource;
    System.Random rnd = new System.Random();

    private void OnEnable() {
        GameEventManager.onGotCollectable += PlaySoundEffect;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        audioArray = new List<AudioClip> {
            audio1,
            audio2
        };
    }

    private void OnDisable() {
        GameEventManager.onGotCollectable -= PlaySoundEffect;
    }

    public void PlaySoundEffect() {
        int r = rnd.Next(audioArray.Count);
        audioSource.PlayOneShot(audioArray[r], grabVolume);
    }
}
