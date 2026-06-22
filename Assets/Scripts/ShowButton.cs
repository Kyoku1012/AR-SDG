using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ShowButton : MonoBehaviour
{
    public GameObject button;
    public GameObject quizPanel;
    ObserverBehaviour observer;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();

        observer.OnTargetStatusChanged += OnStatusChanged;

        button.SetActive(false);
        quizPanel.SetActive(false);
    }

    void OnStatusChanged(ObserverBehaviour behaviour,
                         TargetStatus status)
    {
        bool detected =
            status.Status == Status.TRACKED ||
            status.Status == Status.EXTENDED_TRACKED;

        // Quiz panel should only be shown when the target is detected and the quiz panel is not already active
        button.SetActive(detected && !quizPanel.activeSelf);
        // if (quizPanel.activeSelf)
        // {
        //     button.SetActive(false);
        //     return;
        // }

        // button.SetActive(detected);
    }

    void OnDestroy()
    {
        observer.OnTargetStatusChanged -= OnStatusChanged;
    }
}
