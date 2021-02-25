using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script triggers the fade in/out of the player's page counter.
/// Subscriptions:
/// - onGotCollectable
/// - onGamePaused
/// - onGameWon
/// - onGameLost
/// </summary>
public class Counter : MonoBehaviour
{
    [SerializeField] private float fadeInTime = 0f;
    [SerializeField] private float fadeOutTime = 4f;

    private Text pages;
    private GameEventHandler gameEventHandler;

    private void OnEnable() {
        GameEventManager.onGotCollectable += FlashCounter;
        GameEventManager.onGamePaused += OnPaused;
        GameEventManager.onGameWon += OnGameResult;
        GameEventManager.onGameLost += OnGameResult;
    }

    private void Start() {
        pages = GetComponent<Text>();
        gameEventHandler = GameEventHandler.Instance;

        pages.text = "Collect all " + gameEventHandler.pagesToCollect + " pages";
        pages.CrossFadeAlpha(0, fadeOutTime, false);
    }

    private void OnDisable() {
        GameEventManager.onGotCollectable -= FlashCounter;
        GameEventManager.onGamePaused -= OnPaused;
        GameEventManager.onGameWon -= OnGameResult;
        GameEventManager.onGameLost -= OnGameResult;
    }

    private void FlashCounter() {
        pages.text = "Pages: " + gameEventHandler.pagesCollected + "/" + gameEventHandler.pagesToCollect;
        pages.CrossFadeAlpha(1, fadeInTime, false);
        pages.CrossFadeAlpha(0, fadeOutTime, false);
    }

    private void OnPaused() {
        GetComponentInParent<Canvas>().enabled = !PauseAndShowMenu.Instance.isPaused;
    }

    private void OnGameResult() {
        GetComponentInParent<Canvas>().enabled = false;
    }
}
