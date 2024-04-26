using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupItem : MonoBehaviour, IInteractable
{
    public Item CurrentItem;
    [field: SerializeField] public int Amount { get; set; }

    public Action<Item> ItemPickuped;

    public virtual void Interact()
    {
        ItemPickuped?.Invoke(CurrentItem);
        PlayerInventory.Instance.AddItem(this);
    }
}