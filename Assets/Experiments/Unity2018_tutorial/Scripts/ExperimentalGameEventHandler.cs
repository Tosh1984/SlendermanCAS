using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The game's event handler
public class ExperimentalGameEventHandler : MonoBehaviour
{
    public static ExperimentalGameEventHandler Instance { get; private set; }

    public int pagesCollected = 0;
    public int pagesToCollect = 8;
    public float maxTimeGazingSlender = 5;
    public GameObject slenderman;
    public ExperimentalPlayerController player;

    private XRCardboardController cardboardController;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Only one instance of singleton allowed");
        }

        Instance = this;
    }

    private void OnDestroy() {
        if (Instance == this)
            Instance = null;
    }

    private void Start() {
        cardboardController = XRCardboardController.Instance;
    }

    private void Update() {
        // onGamePaused
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ExperimentalGameEventManager.InvokeGamePaused();
        }

        // onGettingCollectable
        if (cardboardController.IsGettingCollectable()) {
            ExperimentalGameEventManager.InvokeGettingCollectable();
        }

        // onFlashlightToggled
        if (Input.GetKeyDown(KeyCode.F)) {
            ExperimentalGameEventManager.InvokeFlashlightToggle();
        }

        // onGameResult
        if (pagesCollected == pagesToCollect) {
            ExperimentalGameEventManager.InvokeGameResult();

            Debug.Log("***Event: SHOWING GAME RESULT!");
        }
    }
}
