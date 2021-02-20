﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalSlenderSpawn : MonoBehaviour {

    public float spawnOffset = 1f;
    public float spawningTime = 5f;

    Camera mainCamera;
    private Transform slendermanLocation;
    private Transform playerLocation;
    private float timeNotLooking = 0;
    private float viewDistance;
    private float viewAngle;

    private void OnEnable() {
        ExperimentalGameEventManager.onGettingCollectable += Spawn;
    }

    private void Start() {
        ExperimentalGameEventHandler gameManager = ExperimentalGameEventHandler.Instance;

        GameObject slender = gameManager.slenderman;
        ExperimentalPlayerController player = gameManager.player;
        mainCamera = Camera.main;

        slendermanLocation = slender.transform;
        playerLocation = player.transform;
        viewDistance = player.viewDistance;
        viewAngle = player.viewAngle;
    }

    private void Update() {
        float distance = Vector3.Distance(slendermanLocation.position, playerLocation.position);
        float angle = Vector3.Angle(mainCamera.transform.forward, slendermanLocation.position - playerLocation.position);

        if (distance < viewDistance && angle < viewAngle) {
            timeNotLooking = 0;
        } else {
            timeNotLooking += Time.deltaTime;
        }

        if (timeNotLooking > spawningTime) {
            timeNotLooking = 0;
            Spawn();
        }

        slendermanLocation.LookAt(new Vector3(playerLocation.position.x, playerLocation.position.y, playerLocation.position.z));
    }

    private void OnDisable() {
        ExperimentalGameEventManager.onGettingCollectable += Spawn;
    }

    private void Spawn() {
        float minRadius = viewDistance - spawnOffset;
        float maxRadius = viewDistance + spawnOffset;

        RaycastHit hit;
        float randomDistance = UnityEngine.Random.Range(minRadius, maxRadius);
        float randomAngle = UnityEngine.Random.Range(0, 360);

        Vector3 raySpawnPosition = mainCamera.transform.position + new Vector3(randomDistance * Mathf.Cos(randomAngle), 50, randomDistance * Mathf.Sin(randomAngle));

        Ray ray = new Ray(raySpawnPosition, Vector3.down);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (hit.collider != null) {
                slendermanLocation.position = hit.point;
            }
        }
    }
}