using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.Cardboard;

public class CardboardSetup : MonoBehaviour {
    public void Start() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (!Api.HasDeviceParams())
            Api.ScanDeviceParams();
    }

    public void Update() {
        if (Api.IsGearButtonPressed)
            Api.ScanDeviceParams();

        if (Api.IsCloseButtonPressed)
            Application.Quit();

        if (Api.HasNewDeviceParams())
            Api.ReloadDeviceParams();
    }
}
