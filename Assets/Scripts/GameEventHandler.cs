﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages and tracks the game events.
/// Note: this is not for managing player inputs/triggers. See PlayerController.cs
/// Subscriptions:
/// - onGettingCollectable
/// - onPlayerViewEntered
/// Broadcasts:
/// - onGameWon
/// - onGameLost
/// </summary>
public class GameEventHandler : MonoBehaviour
{
    public static GameEventHandler Instance { get; private set; }

    public int pagesCollected = 0;
    public int pagesToCollect = 8;
    public float maxTimeGazingSlender = 5f;
    
    // for difficulty settings
    public bool isTimed = false;
    public float gameTimer = 600; // 10 minutes default
    public float timeElapsed;

    public GameObject slenderman;
    public PlayerController player;
    public GameObject WorldLighting;

    [HideInInspector]
    public bool isGameEnded = false;

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

        if (!isGameEnded) {
            // EVENT: onGameWon
            if (pagesCollected == pagesToCollect) {
                isGameEnded = true;
                GameEventManager.InvokeGameWon();
            }

            // EVENT: onGameLost
            if (timeGazedSlender >= maxTimeGazingSlender) {
                isGameEnded = true;
                GameEventManager.InvokeGameLost();
            } else if (isTimed) {
                if (timeElapsed >= gameTimer) {
                    isGameEnded = true;
                    GameEventManager.InvokeGameLost();
                }
                timeElapsed += Time.deltaTime;
            }
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
