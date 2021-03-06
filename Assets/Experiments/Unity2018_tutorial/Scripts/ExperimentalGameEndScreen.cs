﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Experimental;

namespace Experimental {

    public class ExperimentalGameEndScreen : MonoBehaviour {
        Text title;
        Light worldLight;

        private void OnEnable() {
            ExperimentalGameEventManager.onGameWon += GameWon;
            ExperimentalGameEventManager.onGameLost += GameLost;
        }


        private void Start() {
            GetComponent<Canvas>().enabled = false;
            title = transform.GetChild(0).GetComponent<Text>();
            worldLight = ExperimentalGameEventHandler.Instance.WorldLighting.GetComponent<Light>();
        }

        private void OnDisable() {
            ExperimentalGameEventManager.onGameWon -= GameWon;
            ExperimentalGameEventManager.onGameLost -= GameLost;
        }

        private void GameWon() {
            Time.timeScale = 0;
            AudioListener.pause = true;

            title.text = "You escaped";
            GetComponent<Canvas>().enabled = true;

            worldLight.GetComponent<Animator>().SetTrigger("Won");
        }

        private void GameLost() {
            Time.timeScale = 0;
            AudioListener.pause = true;

            title.text = "";
            GetComponent<Canvas>().enabled = true;

            worldLight.GetComponent<Animator>().SetTrigger("Won");

            // play a slenderman animation
            // analog glitch at 0.3 scan. 0.1 hori shake
            // TODO: replace with a slenderman static video that follows head movement.
        }


    }
}