using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Experimental;

/// <summary>
/// editable variables:
/// - fade in and out time
/// </summary>
namespace Experimental {

    public class ExperimentalCounter : MonoBehaviour {
        [SerializeField] private float fadeInTime = 0f;
        [SerializeField] private float fadeOutTime = 4f;

        private Text pages;
        private ExperimentalGameEventHandler gameEventHandler;

        private void OnEnable() {
            ExperimentalGameEventManager.onGotCollectable += FlashCounter;
            ExperimentalGameEventManager.onGamePaused += OnPaused;
            ExperimentalGameEventManager.onGameWon += OnGameResult;
            ExperimentalGameEventManager.onGameLost += OnGameResult;
        }

        private void Start() {
            pages = GetComponent<Text>();
            gameEventHandler = ExperimentalGameEventHandler.Instance;

            pages.text = "Collect all " + gameEventHandler.pagesToCollect + " pages";
            pages.CrossFadeAlpha(0, fadeOutTime, false);
        }

        private void OnDisable() {
            ExperimentalGameEventManager.onGotCollectable -= FlashCounter;
            ExperimentalGameEventManager.onGamePaused -= OnPaused;
            ExperimentalGameEventManager.onGameWon -= OnGameResult;
            ExperimentalGameEventManager.onGameLost -= OnGameResult;
        }

        private void FlashCounter() {
            pages.text = "Pages: " + gameEventHandler.pagesCollected + "/" + gameEventHandler.pagesToCollect;
            pages.CrossFadeAlpha(1, fadeInTime, false);
            pages.CrossFadeAlpha(0, fadeOutTime, false);
        }

        private void OnPaused() {
            GetComponentInParent<Canvas>().enabled = !ExperimentalPauseAndShowMenu.Instance.isPaused;
        }

        private void OnGameResult() {
            GetComponentInParent<Canvas>().enabled = false;
        }
    }

}
