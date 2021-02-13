using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used for the buttons in the main menu to change game scene or game level.
/// </summary>
public class SceneController : MonoBehaviour
{
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
