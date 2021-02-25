using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static instance for pausing the game.
/// Broadcasts:
/// - onGamePaused
/// </summary>
public class PauseAndShowMenu : MonoBehaviour {

    public static PauseAndShowMenu Instance { get; private set; }
    public bool isPaused = false;

    Light worldLight;
    float initialLighting;
    GameObject parentScreen;

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
        parentScreen = GameObject.Find("Screens");

        GetComponent<Canvas>().enabled = false;
    }

    public void Pause() {
        isPaused = !isPaused;

        // angle Screen canvas to where camera is facing when invoked
        parentScreen.transform.rotation = Camera.main.transform.rotation;

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
