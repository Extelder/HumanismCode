using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingsLanguage : MonoBehaviour
{
    [SerializeField] private DataSettings _settings;

    public void SetRussianLanguage()
    {
        _settings.SelectedLanguage.Value = Language.Russian;
        _settings.DataUpdated();
    }

    public void SetEnglishLanguage()
    {
        _settings.SelectedLanguage.Value = Language.English;
        _settings.DataUpdated();
    }
}