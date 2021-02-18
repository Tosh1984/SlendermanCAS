using System.Collections.Generic;
using UnityEngine;

public class ExperimentalPaperBehavior : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float grabVolume = 1f;

    [SerializeField] private AudioClip audio1;
    [SerializeField] private AudioClip audio2;

    XRCardboardController cardboardController;
    private List<AudioClip> audioArray;
    private AudioSource audioSource;
    System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start() {
        cardboardController = XRCardboardController.Instance;
        audioSource = GetComponent<AudioSource>();

        audioArray = new List<AudioClip> {
            audio1,
            audio2
        };
    }

    // Update is called once per frame
    void Update() {
        if (cardboardController.IsTriggerPressed() && cardboardController.IsCollectableDetected()) {
            PlaySoundEffect();
        }
    }

    //private void OnDestroy() {
    //    PlaySoundEffect();
    //}

    public void Hovering(bool enable) { }

    public void Selecting(bool enable) {

    }

    private void PlaySoundEffect() {
        int r = rnd.Next(audioArray.Count);
        audioSource.PlayOneShot(audioArray[r], grabVolume);
    }
}
