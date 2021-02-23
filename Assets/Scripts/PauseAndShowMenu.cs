using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndShowMenu : MonoBehaviour {

    public static PauseAndShowMenu Instance { get; private set; }
    public bool isPaused = false;

    Light worldLight;
    float initialLighting;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Only one instance of singleton allowed");
        }
        Instance = this;
    }

    private void OnDestroy() {
        if (Instance == this) { Instance = null; }
    }

    private void Start() {
        worldLight = GameEventHandler.Instance.WorldLighting.GetComponent<Light>();
        initialLighting = worldLight.intensity;
        GetComponent<Canvas>().enabled = false;
    }

    public void Pause() {
        isPaused = !isPaused;

        // EVENT: onGamePaused
        GameEventManager.InvokeGamePaused();

        if (isPaused) {
            Time.timeScale = 0;
            AudioListener.pause = true;

            worldLight.intensity = 0f;

            GetComponent<Canvas>().enabled = true;
        } else {
            Time.timeScale = 1;
            AudioListener.pause = false;

            worldLight.intensity = initialLighting;

            GetComponent<Canvas>().enabled = false;
        }
    }
}
