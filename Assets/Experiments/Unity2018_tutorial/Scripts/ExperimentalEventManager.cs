using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalEventManager : MonoBehaviour
{
    // TODO: clean this up
    // what events?
    // - pressed a trigger (for player effects, reticle, page collectable, collected a page)
    // - gazing at slenderman (for player effects, game state)
    // - collected a page (for fog, slenderman agro, game state)
    // - spawn slenderman (for slenderman)

    //public delegate void OnObjectGazed();
    //public static event OnObjectGazed onObjectGazed;
    //public delegate void OnPageCollected();
    //public static event OnPageCollected onPageCollected;
    //public delegate void OnObjectSpawned();
    //public static event OnObjectSpawned onObjectSpawned;

    public delegate void OnGamePaused();
    public static event OnGamePaused onGamePaused;
    public delegate void OnTriggerPressed();
    public static event OnTriggerPressed onTriggerPressed;
    public delegate void OnInteractablesRaycasted();
    public static event OnInteractablesRaycasted onInteractablesRaycasted;
    public delegate void OnViewConeCollided();
    public static event OnViewConeCollided onViewConeCollided;
    public delegate void OnGameStatus();
    public static event OnGameStatus onGameResult;

    public static void InvokeGamePaused() {
        onGamePaused?.Invoke();
    }
    public static void InvokeTriggerPressed() {
        onTriggerPressed?.Invoke();
    }
    public static void InvokeInteractablesRaycasted() {
        onInteractablesRaycasted?.Invoke();
    }
    public static void InvokeWithinFieldOfView() {
        onViewConeCollided?.Invoke();
    }
    public static void InvokeGameResult() {
        onGameResult?.Invoke();
    }
}
