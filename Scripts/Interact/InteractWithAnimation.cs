using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWithAnimation : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 _needCameraRotation;
    [SerializeField] private PlayerCursor _playerCursor;
    [SerializeField] private Transform _needPlayerPosition;
    [SerializeField] private PlayerInteractAnimator _playerInteractAnimator;
    [SerializeField] private UnityEvent _onInteract;
    [SerializeField] private UnityEvent _onInteractAnimationEnd;
    [SerializeField] private UnityEvent _onBacktrack;

    public void Interact()
    {
        PlayerInteractWithAnimation.Instance.Interact(_playerInteractAnimator, _needPlayerPosition.position,
            _needCameraRotation, OnPlayerInteractAnimationEnd);
        _onInteract.Invoke();
        _playerCursor.Enable();
    }

    public void OnPlayerInteractAnimationEnd()
    {
        _onInteractAnimationEnd.Invoke();
    }

    public void Backtrack()
    {
        _onBacktrack.Invoke();
        PlayerInteractWithAnimation.Instance.StopInteraction();
        _playerCursor.Disable();
    }
}