using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Provides the behavior of the Screen object under Player.
/// Subscriptions:
/// - onGameWon
/// - onGameLost
/// </summary>
public class GameEndScreen : MonoBehaviour
{
    Text title;
    Text subtitle;
    Light worldLight;
    GameObject parentScreen;
    GameObject slendermanLoseScreen;
    VideoPlayer videoPlayer;

    private bool isGameLost = false;

    private void OnEnable() {
        GameEventManager.onGameWon += GameWon;
        GameEventManager.onGameLost += GameLost;
    }

    
    private void Start()
    {
        GetComponent<Canvas>().enabled = false;
        title = transform.GetChild(0).GetComponent<Text>();
        subtitle = transform.GetChild(1).GetComponent<Text>();
        worldLight = GameEventHandler.Instance.WorldLighting.GetComponent<Light>();
        parentScreen = GameObject.Find("Screens");
        slendermanLoseScreen = GameObject.Find("SlendermanLoseScreen");
        videoPlayer = slendermanLoseScreen.GetComponent<VideoPlayer>();
    }

    private void Update() {
        if (isGameLost) {
            if (videoPlayer.enabled) {
                parentScreen.transform.rotation = Camera.main.transform.rotation;
            }
            if (videoPlayer.isPaused) {
                videoPlayer.enabled = false;
                AudioListener.pause = true;
                slendermanLoseScreen.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable() {
        GameEventManager.onGameWon -= GameWon;
        GameEventManager.onGameLost -= GameLost;
    }

    private void GameWon() {
        StartCoroutine(WorldLightFadeToBlack());

        Time.timeScale = 0;
        AudioListener.pause = true;

        title.text = "You escaped";
        subtitle.text = "";
        GetComponent<Canvas>().enabled = true;
        parentScreen.transform.rotation = Camera.main.transform.rotation;

        //UnglitchCamera();
    }

    private void GameLost() {
        isGameLost = true;
        StartCoroutine(WorldLightFadeToBlack());

        Time.timeScale = 0;
        //AudioListener.pause = true;

        title.text = "";
        subtitle.text = "Pages: " + GameEventHandler.Instance.pagesCollected + "/" + GameEventHandler.Instance.pagesToCollect + " pages";
        GetComponent<Canvas>().enabled = true;
        parentScreen.transform.rotation = Camera.main.transform.rotation;

        videoPlayer.enabled = true;

        //UnglitchCamera();
    }

    IEnumerator WorldLightFadeToBlack(float fadeDuration = 3f) {
        float timeElapsed = 0f;
        float initialLighting = worldLight.intensity;

        while (timeElapsed < fadeDuration) {
            worldLight.intensity = Mathf.Lerp(initialLighting, 0, timeElapsed / fadeDuration);
            timeElapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }

    //private void UnglitchCamera() {
    //    Kino.AnalogGlitch glitch = Camera.main.GetComponent<Kino.AnalogGlitch>();
    //    glitch.scanLineJitter = 0;
    //    glitch.colorDrift = 0;
    //    glitch.horizontalShake = 0;
    //    glitch.verticalJump = 0;
    //}
}
