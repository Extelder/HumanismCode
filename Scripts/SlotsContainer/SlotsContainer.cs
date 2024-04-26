using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsContainer : MonoBehaviour
{
    [SerializeField] private PlayerCursor _playerCursor;
    [SerializeField] private Item _defaultItem;

    private Canvas _canvas;

    [field: SerializeField] public ContainerSlot[] InventorySlots { get; private set; }
    public bool Opened { get; private set; }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        Opened = _canvas.enabled;

        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlots[i].NumberInOrder = i;
            InventorySlots[i].CurrentItem = _defaultItem;
        }

        Open();
    }

    public virtual void Open()
    {
        Opened = true;
        _canvas.enabled = true;
        _playerCursor.Enable();
    }

    public virtual void Close()
    {
        Opened = false;
        _canvas.enabled = false;
        _playerCursor.Disable();
    }

    public void AddItem(PickupItem item)
    {
        foreach (ContainerSlot inventorySlot in InventorySlots)
        {
            if (inventorySlot.CurrentItem == item.CurrentItem)
            {
                if ((inventorySlot.Amount + item.Amount) > inventorySlot.CurrentItem.MaxAmount)
                {
                    int delta = inventorySlot.CurrentItem.MaxAmount - inventorySlot.Amount;
                    inventorySlot.Amount += delta;
                    item.Amount -= delta;
                    inventorySlot.DataChanged();
                    Debug.Log("Items summ bigger than maxamount");
                    Debug.Log(delta);
                    continue;
                }

                if ((inventorySlot.Amount + item.Amount) <= inventorySlot.CurrentItem.MaxAmount)
                {
                    inventorySlot.Amount += item.Amount;
                    inventorySlot.DataChanged();
                    Debug.Log("Item summ lesser than maxamiunt");
                    return;
                }
            }
        }

        foreach (ContainerSlot inventorySlot in InventorySlots)
        {
            if (inventorySlot.CurrentItem == _defaultItem)
            {
                if (item.Amount + inventorySlot.Amount <= item.CurrentItem.MaxAmount)
                {
                    inventorySlot.ChangeCurrentItem(item);
                    item.Amount = 0;
                    return;
                }

                inventorySlot.Amount = item.CurrentItem.MaxAmount;
                inventorySlot.CurrentItem = item.CurrentItem;
                inventorySlot.DataChanged();

                item.Amount -= item.CurrentItem.MaxAmount;
            }
        }

        if (item.Amount > 0)
        {
            Drop(item.CurrentItem, item.Amount);
        }
    }

    public void AddItem(Item item, int amount, ContainerSlot notIncludeSlot)
    {
        foreach (ContainerSlot inventorySlot in InventorySlots)
        {
            if (inventorySlot.CurrentItem == item && inventorySlot != notIncludeSlot)
            {
                if ((inventorySlot.Amount + amount) > inventorySlot.CurrentItem.MaxAmount)
                {
                    int delta = inventorySlot.CurrentItem.MaxAmount - inventorySlot.Amount;
                    inventorySlot.Amount += delta;
                    amount -= delta;
                    inventorySlot.DataChanged();
                    Debug.Log("Items summ bigger than maxamount");
                    Debug.Log(delta);
                    continue;
                }

                Debug.Log((inventorySlot.Amount + amount));

                if ((inventorySlot.Amount + amount) <= inventorySlot.CurrentItem.MaxAmount)
                {
                    inventorySlot.Amount += amount;
                    inventorySlot.DataChanged();
                    Debug.Log("Item summ lesser than maxamiunt " + inventorySlot.gameObject.name);
                    return;
                }
            }
        }


        foreach (ContainerSlot inventorySlot in InventorySlots)
        {
            if (inventorySlot.CurrentItem == _defaultItem)
            {
                if (amount + inventorySlot.Amount <= item.MaxAmount)
                {
                    inventorySlot.ChangeCurrentItem(item, amount);
                    amount = 0;
                    return;
                }

                inventorySlot.Amount = item.MaxAmount;
                inventorySlot.CurrentItem = item;
                inventorySlot.DataChanged();

                amount -= item.MaxAmount;
            }
        }

        if (amount >= 0)
        {
            Drop(item, amount);
        }
    }

    public void AddItemToEmptySlot(Item item, int amount)
    {
        foreach (ContainerSlot inventorySlot in InventorySlots)
        {
            if (inventorySlot.CurrentItem == _defaultItem)
            {
                if (amount <= 0)
                {
                    return;
                }

                if (amount + inventorySlot.Amount <= item.MaxAmount)
                {
                    inventorySlot.ChangeCurrentItem(item, amount);
                    amount = 0;
                    return;
                }

                inventorySlot.Amount = item.MaxAmount;
                inventorySlot.CurrentItem = item;
                inventorySlot.DataChanged();

                amount -= item.MaxAmount;
            }
        }

        if (amount >= 0)
        {
            Drop(item, amount);
        }
    }

    public bool HaveEmptySlot()
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            if (InventorySlots[i].IsEmpty())
            {
                return true;
            }
        }

        return false;
    }

    private void Drop(Item item, int amount)
    {
    }
}