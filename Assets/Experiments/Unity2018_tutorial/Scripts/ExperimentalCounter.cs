using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentalCounter : MonoBehaviour
{
    [SerializeField] private float fadeInTime = 0f;
    [SerializeField] private float fadeOutTime = 3f;

    private Text pages;
    private ExperimentalGameManager gameManager;
    private XRCardboardController cardboardController;

    // Start is called before the first frame update
    void Start()
    {
        pages = GetComponent<Text>();
        gameManager = GetComponentInParent<ExperimentalGameManager>();
        cardboardController = XRCardboardController.Instance;

        pages.text = "Pages: " + gameManager.pagesCollected + "/" + gameManager.pagesToCollect;
        pages.CrossFadeAlpha(0, fadeOutTime, false);
    }

    // Update is called once per frame
    void Update() {
        pages.text = "Pages: " + gameManager.pagesCollected + "/" + gameManager.pagesToCollect;

        if (cardboardController.IsTriggerPressed() && cardboardController.IsCollectableDetected()) {
            pages.CrossFadeAlpha(1, fadeInTime, false);
            pages.CrossFadeAlpha(0, fadeOutTime, false);
        }
    }
}
