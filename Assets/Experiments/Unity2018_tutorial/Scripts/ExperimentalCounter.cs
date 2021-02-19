using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentalCounter : MonoBehaviour
{
    [SerializeField] private float fadeInTime = 0f;
    [SerializeField] private float fadeOutTime = 3f;

    private Text pages;
    private ExperimentalGameEventHandler gameEventHandler;

    private void OnEnable() {
        ExperimentalGameEventManager.onGettingCollectable += FlashCounter;
    }

    private void Start()
    {
        pages = GetComponent<Text>();
        gameEventHandler = ExperimentalGameEventHandler.Instance;

        pages.text = "Pages: " + gameEventHandler.pagesCollected + "/" + gameEventHandler.pagesToCollect;
        pages.CrossFadeAlpha(0, fadeOutTime, false);
    }

    private void Update() {
        pages.text = "Pages: " + gameEventHandler.pagesCollected + "/" + gameEventHandler.pagesToCollect;
    }

    private void OnDisable() {
        ExperimentalGameEventManager.onGettingCollectable -= FlashCounter;
    }

    void FlashCounter() {
        pages.CrossFadeAlpha(1, fadeInTime, false);
        pages.CrossFadeAlpha(0, fadeOutTime, false);
    }
}
