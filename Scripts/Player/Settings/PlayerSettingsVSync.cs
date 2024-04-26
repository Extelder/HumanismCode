using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsVSync : MonoBehaviour
{
    [SerializeField] private DataSettings _settings;
    [SerializeField] private Toggle _toggle;

    private void Awake()
    {
        _toggle.isOn = _settings.VSync;
        QualitySettings.vSyncCount = Convert.ToInt32(_toggle.isOn);
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(_ => OnToggleValuesChanged());
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(_ => OnToggleValuesChanged());
    }


    public void OnToggleValuesChanged()
    {
        QualitySettings.vSyncCount = Convert.ToInt32(_toggle.isOn);
        _settings.VSync = Convert.ToBoolean(QualitySettings.vSyncCount);
        _settings.DataUpdated();
    }
}