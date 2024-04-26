using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsComplexity : MonoBehaviour
{
    [SerializeField] private PlayerComplexity _complexity;

    [SerializeField] private Image _lowButtonImage;
    [SerializeField] private Image _mediumButtonImage;
    [SerializeField] private Image _highButtonImage;

    [SerializeField] private HighComplexityLevel _highComplexityLevel;
    [SerializeField] private MediumComplexityLevel _mediumComplexityLevel;
    [SerializeField] private LowComplexityLevel _lowComplexityLevel;

    private Image _currentButton;

    private void Start()
    {
        switch (PlayerPrefs.GetString("Complexity"))
        {
            case "Low":
                SetLowComplexity(_lowButtonImage);
                break;
            case "Medium":
                SetMediumComplexity(_mediumButtonImage);
                break;
            case "High":
                SetHighComplexity(_highButtonImage);
                break;
        }
    }

    public void SetLowComplexity(Image button)
    {
        _complexity.ChangeLevel(_lowComplexityLevel);
        SetCurrentComplexityLevelInButton(button);
        PlayerPrefs.SetString("Complexity", "Low");
    }

    public void SetMediumComplexity(Image button)
    {
        _complexity.ChangeLevel(_mediumComplexityLevel);
        SetCurrentComplexityLevelInButton(button);
        PlayerPrefs.SetString("Complexity", "Medium");
    }

    public void SetHighComplexity(Image button)
    {
        _complexity.ChangeLevel(_highComplexityLevel);
        SetCurrentComplexityLevelInButton(button);
        PlayerPrefs.SetString("Complexity", "High");
    }

    private void SetCurrentComplexityLevelInButton(Image button)
    {
        if (_currentButton != null)
            _currentButton.color = Color.white;
        button.color = Color.gray;
        _currentButton = button;
    }
}