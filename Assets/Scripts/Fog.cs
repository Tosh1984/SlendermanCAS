using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float initialFogDensity = 0f;
    [Range(0f, 1f)]
    public float maxFogDensity = 0.3f;
    [Range(1f, 30f)]
    public float fogUpdateDuration = 5f;

    private int pagesToCollect;
    private float currentFogDensity;
    private float fogIterationIncrement;

    private void OnEnable() {
        GameEventManager.onGettingCollectable += UpdateFog;
    }

    private void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0, 0, 0);
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = initialFogDensity;

        pagesToCollect = GameEventHandler.Instance.pagesToCollect;
        currentFogDensity = initialFogDensity;
        fogIterationIncrement = maxFogDensity / pagesToCollect;
    }

    private void OnDisable() {
        GameEventManager.onGettingCollectable -= UpdateFog;
    }

    private void UpdateFog() {
        StartCoroutine(Lerp());
    }

    IEnumerator Lerp() {
        float timeElapsed = 0;
        float valueToLerp = fogIterationIncrement;

        while (timeElapsed < fogUpdateDuration) {
            valueToLerp = Mathf.Lerp(currentFogDensity, fogIterationIncrement, timeElapsed / fogUpdateDuration);
            RenderSettings.fogDensity = valueToLerp;
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        currentFogDensity = valueToLerp;
        fogIterationIncrement += fogIterationIncrement;
    }
}
