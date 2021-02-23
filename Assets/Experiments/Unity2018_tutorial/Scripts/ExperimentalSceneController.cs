using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Experimental;

namespace Experimental {

    public class ExperimentalSceneController : MonoBehaviour {
        public void ChangeScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
