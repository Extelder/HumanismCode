using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject _adviceObject;
    [SerializeField] private PlayerInteract _interact;

    private void OnEnable()
    {
        _interact.InteractableDetected += OnInteractableDetected;
        _interact.InteractableLost += OnInteractableLost;
    }

    private void OnDisable()
    {
        _interact.InteractableDetected -= OnInteractableDetected;
        _interact.InteractableLost -= OnInteractableLost;
    }

    private void OnInteractableDetected()
    {
        _adviceObject.SetActive(true);
    }

    private void OnInteractableLost()
    {
        _adviceObject.SetActive(false);
    }
}