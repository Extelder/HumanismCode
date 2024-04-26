using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnItemPickup : MonoBehaviour
{
    [SerializeField] private PickupItem _pickupItem;

    public UnityEvent OnItemPickupedEvent;

    private void OnEnable()
    {
        _pickupItem.ItemPickuped += OnItemPickuped;
    }

    private void OnDisable()
    {
        _pickupItem.ItemPickuped -= OnItemPickuped;
    }


    private void OnItemPickuped(Item item)
    {
        OnItemPickupedEvent?.Invoke();
    }
}