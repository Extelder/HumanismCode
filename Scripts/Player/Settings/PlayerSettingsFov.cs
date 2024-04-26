using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsFov : MonoBehaviour
{
    [SerializeField] private DataSettings _settings;
    [SerializeField] private Slider _slider;
    [SerializeField] private CinemachineFOV _cinemachineFov;
    [SerializeField] private TextMeshProUGUI _currentSliderValueText;

    private void Awake()
    {
        _slider.value = _settings.FovValue;
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
        _settings.FovValue = Convert.ToInt16(_slider.value);

        _currentSliderValueText.text = _settings.FovValue.ToString();
        _slider.value = Convert.ToInt16(_slider.value);
        _cinemachineFov.DefaultFov = _slider.value;
        _cinemachineFov.SetFOV(_cinemachineFov.DefaultFov);
        _settings.DataUpdated();
    }
}