using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerSettingsEffectsSound : MonoBehaviour
{
    [SerializeField] private DataSettings _settings;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _currentSliderValueText;

    private void Start()
    {
        _slider.value = _settings.EffectsVolume;
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
        _settings.EffectsVolume = _slider.value;
        _settings.DataUpdated();
        _currentSliderValueText.text = _slider.value.ToString();
        _audioMixerGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-50, 0, _slider.value / 100));
    }
}