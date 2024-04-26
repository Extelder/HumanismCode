using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDoorLocked : MonoBehaviour
{
    [SerializeField] private Door _door;

    public UnityEvent OnDoorLockedEvent;

    private void OnEnable()
    {
        _door.Locked += OnDoorLock;
    }

    private void OnDisable()
    {
        _door.Locked -= OnDoorLock;
    }

    private void OnDoorLock()
    {
        OnDoorLockedEvent?.Invoke();
    }
}