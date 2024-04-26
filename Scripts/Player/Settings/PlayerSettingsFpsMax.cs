using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsFpsMax : MonoBehaviour
{
    [SerializeField] private DataSettings _settings;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _currentSliderValueText;

    private void Awake()
    {
        _slider.maxValue = _settings.MaxFpsMax;
        _slider.minValue = 15;
        _slider.value = _settings.FpsMax.Value;
        SliderValueChanged();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(_ => SliderValueChanged());
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(_ => SliderValueChanged());
    }

    private void SliderValueChanged()
    {
        _settings.FpsMax.Value = Convert.ToInt16(_slider.value);
        _settings.DataUpdated();

        _currentSliderValueText.text = _settings.FpsMax.Value.ToString();
    }
}