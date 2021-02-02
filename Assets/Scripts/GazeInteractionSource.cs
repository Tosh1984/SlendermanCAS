using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Google.XR.Cardboard;

public class GazeInteractionSource : MonoBehaviour
{
    [SerializeField] private float intractableDistance = 10;

    private GameObject _gazedObject;
    private PointerEventData _eventData;

    // Start is called before the first frame update
    void Start()
    {
        _eventData = new PointerEventData(EventSystem.current);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInteraction();
    }

    private void UpdateInteraction() {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, intractableDistance)) {
            if (_gazedObject != hit.transform.gameObject) {
                if (_gazedObject) {
                    _gazedObject.GetComponent<IPointerExitHandler>()?.OnPointerExit(_eventData);
                }

                _gazedObject = hit.transform.gameObject;
                _gazedObject.GetComponent<IPointerEnterHandler>()?.OnPointerEnter(_eventData);

            } else if (_gazedObject) {
                _gazedObject.GetComponent<IPointerExitHandler>()?
                                     .OnPointerExit(_eventData);
                _gazedObject = null;
            }
        }

        if (_gazedObject != null && Google.XR.Cardboard.Api.IsTriggerPressed) {
            _gazedObject.GetComponent<IPointerClickHandler>()?.OnPointerClick(_eventData);
        }
    }
}
