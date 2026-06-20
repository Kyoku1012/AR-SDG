using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ShowButton : MonoBehaviour
{
    public GameObject button;
    ObserverBehaviour observer;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();

        observer.OnTargetStatusChanged += OnStatusChanged;

        button.SetActive(false);
    }

    void OnStatusChanged(ObserverBehaviour behaviour,
                         TargetStatus status)
    {
        bool detected =
            status.Status == Status.TRACKED ||
            status.Status == Status.EXTENDED_TRACKED;

        button.SetActive(detected);
    }

    void OnDestroy()
    {
        observer.OnTargetStatusChanged -= OnStatusChanged;
    }
}
