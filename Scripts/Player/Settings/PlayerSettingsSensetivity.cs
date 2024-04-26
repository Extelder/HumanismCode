using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsSensetivity : MonoBehaviour
{
    [SerializeField] private DataSettings _settings;
    [SerializeField] private CinemachineRotation _cinemachineRotation;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _currentSliderValueText;

    private void Start()
    {
        _slider.maxValue = _settings.MaxLookSensetivity;
        _slider.minValue = _settings.MinLookSensetivity;
        _slider.value = _settings.LookSensetivity;
        Debug.Log(_slider.minValue);
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
        _settings.LookSensetivity = _slider.value;
        _settings.DataUpdated();
        float res = ((float) (int) (_settings.LookSensetivity * 100)) / 100;
        _currentSliderValueText.text = res.ToString();
        _cinemachineRotation.UpdateSensetivity(new Vector2(_settings.LookSensetivity, _settings.LookSensetivity));
    }
}