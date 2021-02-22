using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    // Game events
    public delegate void OnGamePaused();
    public static event OnGamePaused onGamePaused;
    public delegate void OnGameWon();
    public static event OnGameWon onGameWon;
    public delegate void OnGameLost();
    public static event OnGameLost onGameLost;

    // Player events
    public delegate void OnGettingCollectable();
    public static event OnGettingCollectable onGettingCollectable;
    public delegate void OnGotCollectable();
    public static event OnGotCollectable onGotCollectable;
    public delegate void OnFlashlightToggle();
    public static event OnFlashlightToggle onFlashlightToggled;
    public delegate void OnPlayerViewEntered();
    public static event OnPlayerViewEntered onPlayerViewEntered;
    public delegate void OnNotPlayerViewEntered();
    public static event OnNotPlayerViewEntered onNotPlayerViewEntered;

    public static void InvokeGamePaused() {
        onGamePaused?.Invoke();
    }
    public static void InvokeGameWon() {
        onGameWon?.Invoke();
    }
    public static void InvokeGameLost() {
        onGameLost?.Invoke();
    }
    public static void InvokeGettingCollectable() {
        onGettingCollectable?.Invoke();
    }
    public static void InvokeGotCollectable() {
        onGotCollectable?.Invoke();
    }
    public static void InvokeFlashlightToggled() {
        onFlashlightToggled?.Invoke();
    }
    public static void InvokePlayerViewEntered() {
        onPlayerViewEntered?.Invoke();
    }
    public static void InvokeNotPlayerViewEntered() {
        onNotPlayerViewEntered?.Invoke();
    }
}
