using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalGameEventManager : MonoBehaviour
{
    public delegate void OnGamePaused();
    public static event OnGamePaused onGamePaused;
    public delegate void OnPlayerFOVEntered();
    public static event OnPlayerFOVEntered onPlayerFOVEntered;
    public delegate void OnGameStatus();
    public static event OnGameStatus onGameResult;
    public delegate void OnFlashlightToggle();
    public static event OnFlashlightToggle onFlashlightToggled;
    public delegate void OnGettingCollectable();
    public static event OnGettingCollectable onGettingCollectable;

    public static void InvokeGamePaused() {
        onGamePaused?.Invoke();
    }
    public static void InvokeGameResult() {
        onGameResult?.Invoke();
    }
    public static void InvokeFlashlightToggle() {
        onFlashlightToggled?.Invoke();
    }
    public static void InvokeGettingCollectable() {
        onGettingCollectable?.Invoke();
    }
}
