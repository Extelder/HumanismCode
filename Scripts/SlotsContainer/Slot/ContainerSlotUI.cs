using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerSlotUI : MonoBehaviour
{
    [SerializeField] private ContainerSlot _slot;

    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _image;

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
        _amountText.text = _slot.Amount.ToString();
        if (_slot.IsEmpty())
        {
            _image.color = new Color(1, 1, 1, 0f);
            return;
        }

        _image.color = new Color(1, 1, 1, 1f);
        _image.sprite = _slot.CurrentItem.Icon;
    }
}