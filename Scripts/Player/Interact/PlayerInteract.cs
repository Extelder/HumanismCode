using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : RaycastBehaviour
{
    private IInteractable _currentInteractItem = null;

    private PlayerControls _controls;

    public event Action InteractableDetected;
    public event Action InteractableLost;

    private void OnEnable()
    {
        _controls = new PlayerControls();
        _controls.Enable();
        _controls.Main.Pickup.performed += context => TryInteract();
        StartCoroutine(Run());
    }

    private void OnDisable()
    {
        _controls.Disable();
        StopCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (GetHitCollider(out Collider collider))
            {
                if (collider.TryGetComponent<IInteractable>(out IInteractable pickupItem))
                {
                    _currentInteractItem = pickupItem;
                    InteractableDetected?.Invoke();
                    continue;
                }
            }

            InteractableLost?.Invoke();
            _currentInteractItem = null;
        }
    }

    private void TryInteract()
    {
        if (_currentInteractItem != null)
            _currentInteractItem.Interact();
    }
}