using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSlot : MonoBehaviour
{
    [SerializeField] private SlotsContainer _slotsContainer;

    public Item DefaultItem { get; private set; }
    public int NumberInOrder { get; set; }
    public Item CurrentItem { get; set; }
    public int Amount { get; set; }

    public event Action ItemValuesChanged;


    private void Start()
    {
        DefaultItem = CurrentItem;
        DataChanged();
    }

    public void DataChanged()
    {
        ItemValuesChanged?.Invoke();
    }

    public void ChangeCurrentItem(PickupItem item)
    {
        CurrentItem = item.CurrentItem;
        Amount = item.Amount;

        DataChanged();
    }

    public void ChangeCurrentItem(Item item, int amount)
    {
        CurrentItem = item;
        Amount = amount;

        DataChanged();
    }

    public void NullifyData()
    {
        CurrentItem = DefaultItem;
        Amount = 0;
        DataChanged();
    }

    public void ExchangeData(ContainerSlot slot)
    {
        if (slot.CurrentItem != CurrentItem || Amount == CurrentItem.MaxAmount ||
            slot.Amount == slot.CurrentItem.MaxAmount)
        {
            Item item = CurrentItem;
            int amount = Amount;

            CurrentItem = slot.CurrentItem;
            Amount = slot.Amount;

            slot.CurrentItem = item;
            slot.Amount = amount;

            slot.DataChanged();
            DataChanged();

            return;
        }

        if (slot.Amount + Amount <= CurrentItem.MaxAmount)
        {
            Amount += slot.Amount;
            DataChanged();
            slot.NullifyData();
            return;
        }

        slot.Amount -= CurrentItem.MaxAmount - Amount;
        Amount = CurrentItem.MaxAmount;
        DataChanged();
        slot.DataChanged();
        if (slot.Amount <= 0)
            slot.NullifyData();
    }

    public bool IsEmpty() => CurrentItem == DefaultItem;

    public void Separate(int secondPartAmount)
    {
        if (_slotsContainer.HaveEmptySlot())
        {
            _slotsContainer.AddItemToEmptySlot(CurrentItem, secondPartAmount);
            Amount -= secondPartAmount;
            DataChanged();
        }
    }

    public void SplitTheItemIntoTwoParts()
    {
        Separate(Amount / 2);
    }
}