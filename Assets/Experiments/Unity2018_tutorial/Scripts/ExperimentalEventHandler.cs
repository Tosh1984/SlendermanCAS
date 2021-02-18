using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is the game's event handler
public class ExperimentalEventHandler : MonoBehaviour
{
    public int pagesCollected = 0;
    public int pagesToCollect = 8;
    [SerializeField] private GameObject slenderman;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera camera;
    [SerializeField] private int slendermanGazeTimeLimit = 4;

    private Time timer;
    private ExperimentalGameCardboardController cardboardController;

    private void Start() {
        cardboardController = ExperimentalGameCardboardController.Instance;
    }

    private void Update() {
        // onGamePaused
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ExperimentalEventManager.InvokeGamePaused();

            Debug.Log("***Event: PAUSED!");
        }

        // onTriggerPressed
        // note: not to be confused with player movement, only for PlayerInteraction
        if (cardboardController.IsTriggerPressed()) {
            ExperimentalEventManager.InvokeTriggerPressed();

            Debug.Log("***Event: TRIGGERED!");
        }

        // onInteractablesRaycasted
        // note: only for interactables. not for slenderman since it's special and you must do a manual raycast on him.
        if (cardboardController.IsInteractableDetected() || cardboardController.IsCollectableDetected()) {
            ExperimentalEventManager.InvokeInteractablesRaycasted();

            Debug.Log("***Event: RAYCASTED!");
        }

        // onConeCollision
        // TODO: this
        //if () {
        //    ExperimentalEventManager.InvokeWithinFieldOfView();

        //    Debug.Log("***Event: CONE COLLIDED!");
        //}

        // onGameResult
        if (pagesCollected == pagesToCollect) {
            ExperimentalEventManager.InvokeGameResult();

            Debug.Log("***Event: GAME RESULT!");
        }
    }
}
