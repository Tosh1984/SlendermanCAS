using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Experimental;

namespace Experimental {

    public class ExperimentalFog : MonoBehaviour {
        public float initialFogDensity = 0f;
        [Range(0f, 1f)]
        public float maxFogDensity = 1f;
        [Range(1f, 30f)]
        public float fogUpdateDuration;

        private int pagesToCollect;
        private float currentFogDensity;
        private float fogIterationIncrement;

        private void OnEnable() {
            ExperimentalGameEventManager.onGettingCollectable += UpdateFog;
        }

        private void Start() {
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = initialFogDensity;

            pagesToCollect = ExperimentalGameEventHandler.Instance.pagesToCollect;
            currentFogDensity = initialFogDensity;
            fogIterationIncrement = maxFogDensity / pagesToCollect;
        }

        private void OnDisable() {
            ExperimentalGameEventManager.onGettingCollectable -= UpdateFog;
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
}