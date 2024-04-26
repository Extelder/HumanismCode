using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedSlotUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _selectedImage;
    [SerializeField] private SelectedSlot _slot;
    [SerializeField] private ContainerSlot containerSlot;
    [SerializeField] private Button _separateSlotButton;
    [SerializeField] private Button _splitSlotButton;
    [SerializeField] private Button _dropSlotButton;
    [SerializeField] private TextMeshProUGUI _currentSeparateValueTextComponent;
    [SerializeField] private int _currentSeparateSliderMaxValue;

    private void OnEnable()
    {
        _slot.Selected += OnSelected;
        _slot.DeSelected += OnDeSelected;

        containerSlot.ItemValuesChanged += OnItemValuesChanged;
    }

    private void OnDisable()
    {
        _slot.Selected -= OnSelected;
        _slot.DeSelected -= OnDeSelected;

        containerSlot.ItemValuesChanged -= OnItemValuesChanged;
    }

    private void OnItemValuesChanged()
    {
        if (!containerSlot.CurrentItem.CanSplited)
            return;

        if (PlayerInventory.SelectedSlot != _slot)
            return;

        if (containerSlot.Amount - 1 <= 0)
        {
            _currentSeparateValueTextComponent.gameObject.SetActive(false);

            _separateSlotButton.gameObject.SetActive(false);
            _splitSlotButton.gameObject.SetActive(false);
            _dropSlotButton.gameObject.SetActive(false);

            _slider.gameObject.SetActive(false);
            return;
        }

        _currentSeparateValueTextComponent.gameObject.SetActive(true);

        _separateSlotButton.gameObject.SetActive(true);
        _splitSlotButton.gameObject.SetActive(true);
        _dropSlotButton.gameObject.SetActive(true);

        _slider.gameObject.SetActive(true);

        _currentSeparateSliderMaxValue = containerSlot.Amount - 1;
        _slider.maxValue = _currentSeparateSliderMaxValue;
    }

    public void OnSelected()
    {
        _selectedImage.gameObject.SetActive(true);
        _dropSlotButton.gameObject.SetActive(true);

        OnItemValuesChanged();
        if (containerSlot.CurrentItem.CanSplited && containerSlot.Amount - 1 > 0)
        {
            _currentSeparateValueTextComponent.gameObject.SetActive(true);

            _separateSlotButton.gameObject.SetActive(true);
            _splitSlotButton.gameObject.SetActive(true);

            _slider.gameObject.SetActive(true);

            _currentSeparateSliderMaxValue = containerSlot.Amount - 1;
            _slider.maxValue = _currentSeparateSliderMaxValue;

            _slider.onValueChanged.AddListener(_ =>
                _currentSeparateValueTextComponent.text = Convert.ToInt32(_slider.value).ToString());
        }
    }

    public void OnDeSelected()
    {
        _selectedImage.gameObject.SetActive(false);
        _dropSlotButton.gameObject.SetActive(false);

        OnItemValuesChanged();

        _currentSeparateValueTextComponent.gameObject.SetActive(false);

        _slider.onValueChanged.RemoveAllListeners();

        _separateSlotButton.gameObject.SetActive(false);
        _splitSlotButton.gameObject.SetActive(false);

        _slider.gameObject.SetActive(false);
    }
}