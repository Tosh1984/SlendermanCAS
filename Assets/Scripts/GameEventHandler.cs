using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages and tracks the game events, including...
/// - 
/// Note: this is not for managing player inputs/triggers. See PlayerController.
/// </summary>
public class GameEventHandler : MonoBehaviour
{
    public static GameEventHandler Instance { get; private set; }

    public int pagesCollected = 0;
    public int pagesToCollect = 8;
    public float maxTimeGazingSlender = 5f;
    
    // for difficulty settings
    public bool isTimed = false;
    public float gameTimer = 1800; // 30 minutes
    public float timeElapsed;
    // todo: apply this?
    public bool isPageSpawnedRandomly = false;

    public GameObject slenderman;
    public PlayerController player;
    public GameObject WorldLighting;

    private float timeGazedSlender = 0f;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Only one instance of singleton allowed");
        }
        Instance = this;
    }

    private void OnDestroy() {
        if (Instance == this) { Instance = null; }
    }

    private void OnEnable() {
        GameEventManager.onGettingCollectable += PageCollected;
        GameEventManager.onPlayerViewEntered += GazingSlenderman;
    }

    private void Start() {  }

    private void Update() {
        // EVENT: onGameWon
        if (pagesCollected == pagesToCollect) {
            GameEventManager.InvokeGameWon();
        }

        // EVENT: onGameLost
        if (timeGazedSlender >= maxTimeGazingSlender) {
            GameEventManager.InvokeGameLost();
        } else if (isTimed) {
            if (timeElapsed >= gameTimer) {
                GameEventManager.InvokeGameLost();
            }
            timeElapsed += Time.deltaTime;
        }
    }

    private void OnDisable() {
        GameEventManager.onGettingCollectable -= PageCollected;
        GameEventManager.onPlayerViewEntered -= GazingSlenderman;
    }

    private void PageCollected() {
        pagesCollected += 1;
    }

    private void GazingSlenderman() {
        if (!PauseAndShowMenu.Instance.isPaused) {
            timeGazedSlender += Time.deltaTime;
        }
    }
}
