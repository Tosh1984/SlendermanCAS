using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// cardboard ui input hack 
// use XR Interaction Toolkit Interactor to detect pointer enter/exit (hover)
// add this script to a Button UI element to handle clicks.
// (Already added by checking make buttons clickable in XRCardboardController)
// requires theres also a XRCardboardController in the scene
public class CardboardButtonClickable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//ISelectHandler, IPointerClickHandler, 
{
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("**** OnPointerEnter " + gameObject.name);
        XRCardboardController.Instance.OnTriggerPressed.AddListener(OnClick);
        XRCardboardController.Instance.SetButtonDetectedAs(true);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("**** OnPointerExit " + gameObject.name);
        XRCardboardController.Instance.OnTriggerPressed.RemoveListener(OnClick);
        XRCardboardController.Instance.SetButtonDetectedAs(false);
    }

    private void OnClick()
    {
        Debug.Log("*** Invoking OnClick");
        _button.onClick.Invoke();
    }


    //public void OnPointerClick(PointerEventData eventData) {
    //    Debug.Log("**** OnPointerClick");
    //}

    //public void OnSelect(BaseEventData eventData)
    //{
    //    Debug.Log("**** OnSelect");
    //}
}
