using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExperimentalGameCardboardInteractable : CardboardInteractable
{
    new void Update()
    {
        if (ExperimentalGameCardboardController.Instance.IsTriggerPressed())
        {
            if (_isSelecting)
            {
                _isSelecting = false;
                onSelectExit.Invoke();
            }
            else if (_isHovering)
            {
                _isSelecting = true;
                onSelectEnter.Invoke();
            }
        }
    }
}
