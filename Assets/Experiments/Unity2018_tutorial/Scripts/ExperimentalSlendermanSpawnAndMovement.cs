using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Experimental;

namespace Experimental {

    public class ExperimentalSlendermanSpawnAndMovement : MonoBehaviour {

        public float spawningTime = 8f;
        public float minSpawningTime = 1f;

        Camera mainCamera;
        private Transform slendermanLocation;
        private Transform playerLocation;
        private float timeNotLooking = 0f;
        private float spawnRadius;

        private ExperimentalGameEventHandler gameManager;

        private void OnEnable() {
            ExperimentalGameEventManager.onGotCollectable += Spawn;
            ExperimentalGameEventManager.onPlayerViewEntered += SlendermanGazed;
            ExperimentalGameEventManager.onNotPlayerViewEntered += SlendermanNotGazed;
        }

        private void Start() {
            gameManager = ExperimentalGameEventHandler.Instance;

            GameObject slender = gameManager.slenderman;
            ExperimentalPlayerController player = gameManager.player;
            mainCamera = Camera.main;

            slendermanLocation = slender.transform;
            playerLocation = player.transform;
            spawnRadius = player.viewDistance;
        }

        private void Update() {
            slendermanLocation.LookAt(new Vector3(playerLocation.position.x, 0, playerLocation.position.z));
        }

        private void OnDisable() {
            ExperimentalGameEventManager.onGotCollectable -= Spawn;
            ExperimentalGameEventManager.onPlayerViewEntered -= SlendermanGazed;
            ExperimentalGameEventManager.onNotPlayerViewEntered -= SlendermanNotGazed;
        }

        private void SlendermanGazed() {
            timeNotLooking = 0;
        }

        private void SlendermanNotGazed() {
            timeNotLooking += Time.deltaTime;
            if (timeNotLooking > spawningTime) {
                timeNotLooking = 0;
                Spawn();
            }
        }

        private void Spawn() {
            RaycastHit hit;

            float adjustment = AdjustSpawning();
            spawningTime *= adjustment;
            spawningTime = (spawningTime < minSpawningTime) ? minSpawningTime : spawningTime;
            float adjustedDistance = spawnRadius * adjustment;
            float randomAngle = UnityEngine.Random.Range(0, 360);

            Vector3 raySpawnPosition = mainCamera.transform.position + new Vector3(adjustedDistance * Mathf.Cos(randomAngle), 50, adjustedDistance * Mathf.Sin(randomAngle));

            Ray ray = new Ray(raySpawnPosition, Vector3.down);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.collider != null) {
                    slendermanLocation.position = hit.point;
                }
            }
        }

        // function of the % of spawn time and radius = 1 - max(gameTime quo, pagesCollected quo)
        private float AdjustSpawning() {
            float gT = gameManager.timeElapsed / gameManager.gameTimer;
            float pC = gameManager.pagesCollected / gameManager.pagesToCollect;

            return 1 - Math.Max(gT, pC);
        }
    }
}