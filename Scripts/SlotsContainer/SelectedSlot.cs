using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Slider _slider;
    [SerializeField] private ContainerSlot _slot;
    [SerializeField] private Button _separateSlotButton;
    [SerializeField] private Button _splitSlotButton;
    [SerializeField] private Button _dropSlotButton;

    public event Action Selected;
    public event Action DeSelected;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerInventory.SelectedSlot != null)
        {
            PlayerInventory.SelectedSlot.DeSelect();
        }

        if (!_slot.IsEmpty())
            Select();
    }

    private void OnEnable()
    {
        _slot.ItemValuesChanged += OnItemValuesChanged;
    }

    private void OnDisable()
    {
        _slot.ItemValuesChanged -= OnItemValuesChanged;
    }

    private void OnItemValuesChanged()
    {
        if (_slot.IsEmpty())
            DeSelect();
    }

    public void Select()
    {
        Selected.Invoke();
        PlayerInventory.SelectedSlot = this;
        _dropSlotButton.onClick.AddListener(() =>
        {
            PlayerDrop.Instance.Drop(_slot.CurrentItem, _slot.Amount);
            _slot.NullifyData();
        });

        if (_slot.CurrentItem.CanSplited && _slot.Amount - 1 > 0)
        {
            _separateSlotButton.onClick.AddListener(() =>
            {
                Debug.Log("select");
                _slot.Separate(Convert.ToInt32(_slider.value));
            });
            _splitSlotButton.onClick.AddListener(() => _slot.SplitTheItemIntoTwoParts());
        }
    }

    public void DeSelect()
    {
        PlayerInventory.SelectedSlot = null;
        _separateSlotButton.onClick.RemoveAllListeners();
        _splitSlotButton.onClick.RemoveAllListeners();
        _dropSlotButton.onClick.RemoveAllListeners();

        DeSelected?.Invoke();
    }
}