using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerSettingsQuality : MonoBehaviour
{
    [SerializeField] private DataSettings _dataSettings;
    [SerializeField] private RenderPipelineAsset _lowRender;
    [SerializeField] private RenderPipelineAsset _mediumRender;
    [SerializeField] private RenderPipelineAsset _highRender;

    [SerializeField] private Image _lowButtonImage;
    [SerializeField] private Image _mediumButtonImage;
    [SerializeField] private Image _highButtonImage;

    private Image _currentButton;

    private void Start()
    {
        switch (_dataSettings.SelectedQuality.Value.ToString())
        {
            case "Low":
                SetLowQuality(_lowButtonImage);
                break;
            case "Medium":
                SetMediumQuality(_mediumButtonImage);
                break;
            case "High":
                SetHighQuality(_highButtonImage);
                break;
        }
    }

    public void SetLowQuality(Image button)
    {
        _dataSettings.SelectedQuality.Value = Quality.Low;
        SetCurrentQualityLevelInButton(button);
        QualitySettings.renderPipeline = _lowRender;
    }

    public void SetMediumQuality(Image button)
    {
        _dataSettings.SelectedQuality.Value = Quality.Medium;
        SetCurrentQualityLevelInButton(button);
        QualitySettings.renderPipeline = _mediumRender;
    }

    public void SetHighQuality(Image button)
    {
        _dataSettings.SelectedQuality.Value = Quality.High;
        SetCurrentQualityLevelInButton(button);
        QualitySettings.renderPipeline = _highRender;
    }

    private void SetCurrentQualityLevelInButton(Image button)
    {
        if (_currentButton != null)
            _currentButton.color = Color.white;
        button.color = Color.gray;
        _currentButton = button;
        _dataSettings.DataUpdated();
    }
}