using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndScreen : MonoBehaviour
{
    Text title;
    Light worldLight;
    GameObject parentScreen;

    private void OnEnable() {
        GameEventManager.onGameWon += GameWon;
        GameEventManager.onGameLost += GameLost;
    }

    
    private void Start()
    {
        GetComponent<Canvas>().enabled = false;
        title = transform.GetChild(0).GetComponent<Text>();
        worldLight = GameEventHandler.Instance.WorldLighting.GetComponent<Light>();
        parentScreen = GameObject.Find("Screens");
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
        GetComponent<Canvas>().enabled = true;
        parentScreen.transform.rotation = Camera.main.transform.rotation;

        //worldLight.GetComponent<Animator>().SetTrigger("Won");
    }

    private void GameLost() {
        StartCoroutine(WorldLightFadeToBlack());

        Time.timeScale = 0;
        //AudioListener.pause = true;

        title.text = "";
        GetComponent<Canvas>().enabled = true;
        parentScreen.transform.rotation = Camera.main.transform.rotation;

        //worldLight.GetComponent<Animator>().SetTrigger("Won");

        // play a slenderman animation
        // analog glitch at 0.3 scan. 0.1 hori shake
        // TODO: replace with a slenderman static video that follows head movement.
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
}
