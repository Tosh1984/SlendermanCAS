using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentalCounter : MonoBehaviour
{
    private Text pagesCollected;
    private ExperimentalPlayerController player;
    private XRCardboardController cardboardController;

    // Start is called before the first frame update
    void Start()
    {
        pagesCollected = GetComponent<Text>();
        player = GetComponentInParent<ExperimentalPlayerController>();
        cardboardController = XRCardboardController.Instance;
    }

    // Update is called once per frame
    void Update() {
        pagesCollected.text = "Pages: " + player.collectedPages + "/" + player.pagesToCollect;
    }
}
