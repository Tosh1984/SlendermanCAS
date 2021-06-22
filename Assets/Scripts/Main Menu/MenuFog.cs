using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fog effect for main menu
/// </summary>
public class MenuFog : MonoBehaviour
{
    public float initialFogDensity = 0f;

    private void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0, 0, 0);
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = initialFogDensity;
    }
}
