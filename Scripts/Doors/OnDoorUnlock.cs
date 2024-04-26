using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDoorUnlock : MonoBehaviour
{
    [SerializeField] private Door _door;

    public UnityEvent OnDoorUnlockedEvent;

    private void OnEnable()
    {
        _door.Unlocked += OnDoorUnlocked;
    }

    private void OnDisable()
    {
        _door.Unlocked -= OnDoorUnlocked;
    }

    private void OnDoorUnlocked()
    {
        OnDoorUnlockedEvent?.Invoke();
    }
}