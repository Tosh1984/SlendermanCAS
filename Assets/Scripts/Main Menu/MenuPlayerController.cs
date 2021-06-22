using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// A simplified PlayerController just for the Main Menu scene.
/// </summary>
public class MenuPlayerController : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
        float maxAngle = 175f;
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;

        Camera.main.transform.localRotation = Quaternion.Euler(
            new Vector4(
                Mathf.Clamp(-1f * (mouseY * 180f), -maxAngle, maxAngle),
                Mathf.Clamp(mouseX * 360f, -maxAngle, maxAngle),
                transform.localRotation.z
                )
            );
#endif
    }
}
