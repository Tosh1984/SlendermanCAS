using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalShowMenu : MonoBehaviour {

    private bool isPaused = false;

    private void OnEnable() {
        ExperimentalGameEventManager.onGamePaused += Pause;
    }

    private void OnDisable() {
        ExperimentalGameEventManager.onGamePaused -= Pause;
    }

    // todo: display a menu screen 
    private void Pause() {
        isPaused = !isPaused;

        if (isPaused) {
            Time.timeScale = 0;
            Debug.Log("PAUSED");
        } else {
            Time.timeScale = 1;
            Debug.Log("RESUMED");
        }
    }
}
