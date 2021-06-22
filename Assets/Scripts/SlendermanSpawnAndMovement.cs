using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the spawning behavior of slenderman
/// Subscriptions:
/// - onGotCollectable
/// - onPlayerViewEntered
/// - onNotPlayerViewEntered
/// </summary>
public class SlendermanSpawnAndMovement : MonoBehaviour {

    public float spawningTime = 8f;
    public float minSpawningTime = 1f;

    Camera mainCamera;
    private Transform slendermanLocation;
    private Transform playerLocation;
    private float timeNotLooking = 0f;
    private float spawnRadius;

    private GameEventHandler gameManager;

    private void OnEnable() {
        GameEventManager.onGotCollectable += Spawn;
        GameEventManager.onPlayerViewEntered += SlendermanGazed;
        GameEventManager.onNotPlayerViewEntered += SlendermanNotGazed;
    }

    private void Start() {
        gameManager = GameEventHandler.Instance;

        GameObject slender = gameManager.slenderman;
        PlayerController player = gameManager.player;
        mainCamera = Camera.main;

        slendermanLocation = slender.transform;
        playerLocation = player.transform;
        spawnRadius = player.viewDistance;
    }

    private void Update() {
        slendermanLocation.LookAt(new Vector3(playerLocation.position.x, slendermanLocation.position.y, playerLocation.position.z));
    }

    private void OnDisable() {
        GameEventManager.onGotCollectable -= Spawn;
        GameEventManager.onPlayerViewEntered -= SlendermanGazed;
        GameEventManager.onNotPlayerViewEntered -= SlendermanNotGazed;
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
            if (hit.collider != null && hit.transform.CompareTag("Ground")) {
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
