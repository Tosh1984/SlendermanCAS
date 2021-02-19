using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// cardboard ui input hack 
// use XR Interaction Toolkit Interactor to detect pointer enter/exit (hover)
// add this script to a Button UI element to handle clicks. 
// requires theres also a XRCardboardController in the scene
public class ExperimentalGameCardboardButtonClickable : CardboardButtonClickable
{

    ExperimentalGameCardboardController cardboardController;

    void Start()
    {
        _button = GetComponent<Button>();
        cardboardController = ExperimentalGameCardboardController.Instance;
    }

    public new void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("** OnPointerEnter " + gameObject.name);
        cardboardController.OnTriggerPressed.AddListener(OnClick);
        cardboardController.SetButtonDetectedAs(true);
    }


    public new void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("** OnPointerExit " + gameObject.name);
        cardboardController.OnTriggerPressed.RemoveListener(OnClick);
        cardboardController.SetButtonDetectedAs(false);
    }


    //public void OnPointerClick(PointerEventData eventData) {
    //    Debug.Log("**** OnPointerClick");
    //}

    //public void OnSelect(BaseEventData eventData)
    //{
    //    Debug.Log("**** OnSelect");
    //}
}
