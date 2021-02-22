using System.Collections;
using System.Collections.Generic;
#if !UNITY_EDITOR
using Google.XR.Cardboard;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// XR Cardboard controller and interactor
/// <summary>
/// Modified version of the controller.
/// Added helper functions explicitly track the state of the gazed object.
/// </summary>
public class XRCardboardController : MonoBehaviour
{
    public bool makeAllButtonsClickable = true;
    public bool raycastEveryUpdate = true;
    public LayerMask interactablesLayers = -1;
    public float maxInteractionDistance = 10f;

    public UnityEvent OnTriggerPressed = new UnityEvent();
    
    private Camera camera;
    private GameObject _gazedAtObject = null;

    public static XRCardboardController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Only one instance of singleton allowed");
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        if (camera == null)
            camera = Camera.main;

        SetupCardboard();

        if (makeAllButtonsClickable)
            _MakeAllButtonsClickable();
    }

    private void Update()
    {
        if (raycastEveryUpdate)
        {
            _CastForInteractables();
        }

        if (IsTriggerPressed())
        {
            OnClick();
        }

#if !UNITY_EDITOR
        if (Api.IsCloseButtonPressed)
        {
            ApplicationQuit();
        }

        if (Api.IsGearButtonPressed)
        {
            Api.ScanDeviceParams();
        }

        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }
#endif
    }

    public void OnClick()
    {
        //Debug.Log("**** in OnClick");
        OnTriggerPressed.Invoke();
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }


    private void SetupCardboard()
    {
#if !UNITY_EDITOR
        // Configures the app to not shut down the screen and sets the brightness to maximum.
        // Brightness control is expected to work only in iOS, see:
        // https://docs.unity3d.com/ScriptReference/Screen-brightness.html.
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;

        // Checks if the device parameters are stored and scans them if not.
        if (!Api.HasDeviceParams())
        {
            Api.ScanDeviceParams();
        }
#endif
    }

    private void _MakeAllButtonsClickable()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.AddComponent<CardboardButtonClickable>();
        }
    }

    private void _CastForInteractables()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxInteractionDistance, interactablesLayers))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                Debug.Log("New gazed object: " + hit.transform.gameObject.name);

                _gazedAtObject?.SendMessage("PointerExit");
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("PointerEnter");
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("PointerExit");
            _gazedAtObject = null;
        }
    }

    public bool IsTriggerPressed() {
#if UNITY_EDITOR
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E);
#else
            return (Api.IsTriggerPressed || Input.GetMouseButtonDown(0)) || Input.GetKeyDown(KeyCode.E);
#endif
    }

    // MARK: Added functionalities
    private bool isButtonDetected = false;
    public void SetButtonDetectedAs(bool val) {
        isButtonDetected = val;
    }

    public bool IsInteractableDetected() {
        return (_gazedAtObject != null) || isButtonDetected;
    }

    public bool IsCollectableDetected() {
        return (_gazedAtObject != null) && _gazedAtObject.CompareTag("Collectable");
    }

    public bool IsGettingCollectable() {
        if (IsTriggerPressed() && IsCollectableDetected()) {
            return true;
        }
        return false;
    }

    public void GetCollectable() {
        if (IsGettingCollectable()) {
            _gazedAtObject?.SendMessage("PointerExit");
            Destroy(_gazedAtObject);
            _gazedAtObject = null;
        }
    }

    public GameObject GetGazedObject() {
        return _gazedAtObject;
    }
}
