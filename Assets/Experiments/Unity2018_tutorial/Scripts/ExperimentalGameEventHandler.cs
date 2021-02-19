using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is the game's event handler
// it'll track any triggers: key presses, or game states so it can broadcast to subscribers
public class ExperimentalGameEventHandler : MonoBehaviour
{
    public static ExperimentalGameEventHandler Instance { get; private set; }

    public int pagesCollected = 0;
    public int pagesToCollect = 8;
    [SerializeField] private GameObject slenderman;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera camera;
    [SerializeField] private int slendermanGazeTimeLimit = 4;

    private Time timer;
    private ExperimentalGameCardboardController cardboardController;

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
        cardboardController = ExperimentalGameCardboardController.Instance;
    }

    private void Update() {
        // onGamePaused
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ExperimentalGameEventManager.InvokeGamePaused();

            Debug.Log("***Event: PAUSING!");
        }

        // onTriggerPressed
        // note: not to be confused with player movement, only for PlayerInteraction
        //if (cardboardController.IsTriggerPressed()) {
        //    ExperimentalGameEventManager.InvokeTriggerPressed();

        //    Debug.Log("***Event: TRIGGERING!");
        //}

        // onGettingCollectable
        if (cardboardController.IsGettingCollectable()) {
            ExperimentalGameEventManager.InvokeGettingCollectable();

            Debug.Log("***Event: COLLECTING!");
        }

        // onInteractablesRaycasted
        // note: only for interactables. not for slenderman since it's special and you must do a manual raycast on him.
        //if (cardboardController.IsInteractableDetected() || cardboardController.IsCollectableDetected()) {
        //    ExperimentalGameEventManager.InvokeInteractablesRaycasted();

        //    Debug.Log("***Event: INTERACTABLE!");
        //}

        // onConeCollision
        // TODO: this
        //if () {
        //    ExperimentalEventManager.InvokeWithinFieldOfView();

        //    Debug.Log("***Event: CONE COLLIDING!");
        //}

        // onFlashlightToggled
        if (Input.GetKeyDown(KeyCode.F)) {
            ExperimentalGameEventManager.InvokeFlashlightToggle();

            Debug.Log("***Event: FLASHLIGHT OPENING!");
        }

        // onGameResult
        if (pagesCollected == pagesToCollect) {
            ExperimentalGameEventManager.InvokeGameResult();

            Debug.Log("***Event: SHOWING GAME RESULT!");
        }
    }

    // TODO: implement clock here for slenderman spawning
}
