using UnityEngine;

namespace Managers {
    public class GameManager : MonoBehaviour {
        [SerializeField] private Transform player;

        private static GameManager privateInstance;

        public static GameManager Instance {
            get {
                if (privateInstance == null) {
                    privateInstance = new GameObject().AddComponent<GameManager>();
                }

                return privateInstance;
            }
        }

        private void Awake() {
            if (privateInstance != null && privateInstance != this) {
                Destroy(gameObject);
            } else {
                privateInstance = this;
            }
        }

        private void OnDestroy() {
            if (privateInstance == this) {
                privateInstance = null;
            }
        }

        public Transform GetPlayer() {
            return player;
        }
    }
}