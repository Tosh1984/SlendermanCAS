using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Interactions {
    public class GazeInteractionSource : MonoBehaviour {
        [SerializeField] private float intractableDistance = 10;

        [SerializeField] private UnityEvent onFocusIntractable;
        [SerializeField] private UnityEvent onLoseFocus;
        [SerializeField] private UnityEvent onClick;

        private GameObject gazedObject;
        private PointerEventData eventData;

        private void Start() {
            eventData = new PointerEventData(EventSystem.current);
        }

        public void Update() {
            UpdateInteraction();
        }

        private void UpdateInteraction() {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, intractableDistance)) {
                if (gazedObject != hit.transform.gameObject) {
                    if (gazedObject) {
                        gazedObject.GetComponentInParent<IPointerExitHandler>()?.OnPointerExit(eventData);

                        if (IsGazedObjectIntractable()) {
                            onLoseFocus?.Invoke();
                        }
                    }

                    gazedObject = hit.transform.gameObject;
                    gazedObject.GetComponentInParent<IPointerEnterHandler>()?.OnPointerEnter(eventData);

                    if (IsGazedObjectIntractable()) {
                        onFocusIntractable?.Invoke();
                    }
                }
            } else if (gazedObject) {
                gazedObject.GetComponentInParent<IPointerExitHandler>()?.OnPointerExit(eventData);
                gazedObject = null;
                onLoseFocus?.Invoke();
            }

            // TODO: change Input.GetMouseButtonDown?
            if (gazedObject != null && (Api.IsTriggerPressed || Input.GetMouseButtonDown(0))) {
                var clickHandler = gazedObject.GetComponentInParent<IPointerClickHandler>();

                if (clickHandler != null) {
                    eventData.pointerPressRaycast = new RaycastResult {
                        worldPosition = hit.point
                    };

                    clickHandler.OnPointerClick(eventData);
                }

                if (IsGazedObjectIntractable()) {
                    onClick?.Invoke();
                }
            }
        }

        private bool IsGazedObjectIntractable() {
            return gazedObject.GetComponentInParent<IEventSystemHandler>() != null;
        }
    }
}