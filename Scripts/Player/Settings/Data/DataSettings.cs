using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public enum Language
{
    Russian,
    English
}

public enum Quality
{
    Low,
    Medium,
    High
}

[CreateAssetMenu(fileName = "Settings", menuName = "Data/Settings")]
public class DataSettings : ScriptableObject
{
    public float LookSensetivity;
    public ReactiveProperty<int> FpsMax = new ReactiveProperty<int>();
    public int MaxFpsMax;
    public float MaxLookSensetivity;
    public float MinLookSensetivity;
    public float FovValue;
    public float AllMusicVolume;
    public float MusicVolume;
    public float EffectsVolume;
    public bool VSync = false;
    public ReactiveProperty<Language> SelectedLanguage = new ReactiveProperty<Language>();
    public ReactiveProperty<Quality> SelectedQuality = new ReactiveProperty<Quality>();

    public Action DataChanged;

    public void DataUpdated()
    {
        Save();
        Debug.Log("DataUpdated");
    }

    public void Save()
    {
        PlayerPrefs.SetString(nameof(SelectedQuality), SelectedQuality.Value.ToString());
        PlayerPrefs.SetString(nameof(SelectedLanguage), SelectedLanguage.Value.ToString());
        PlayerPrefs.SetFloat(nameof(AllMusicVolume), AllMusicVolume);
        PlayerPrefs.SetFloat(nameof(MusicVolume), MusicVolume);
        PlayerPrefs.SetFloat(nameof(EffectsVolume), EffectsVolume);
        PlayerPrefs.SetFloat(nameof(LookSensetivity), LookSensetivity);
        PlayerPrefs.SetFloat(nameof(MaxLookSensetivity), MaxLookSensetivity);
        PlayerPrefs.SetFloat(nameof(MinLookSensetivity), MinLookSensetivity);
        PlayerPrefs.SetFloat(nameof(FovValue), FovValue);
        PlayerPrefs.SetInt(nameof(FpsMax), FpsMax.Value);
        PlayerPrefs.SetInt(nameof(VSync), Convert.ToInt16(VSync));
        DataChanged?.Invoke();
    }

    public void Load()
    {
        if (PlayerPrefs.GetString(nameof(SelectedLanguage), "English") == "Russian")
        {
            SelectedLanguage.Value = Language.Russian;
        }
        else
        {
            SelectedLanguage.Value = Language.English;
        }

        switch (PlayerPrefs.GetString(nameof(SelectedQuality), "High"))
        {
            case "Low":
                SelectedQuality.Value = Quality.Low;
                break;
            case "Medium":
                SelectedQuality.Value = Quality.Medium;
                break;
            case "High":
                SelectedQuality.Value = Quality.High;
                break;
        }

        LookSensetivity = PlayerPrefs.GetFloat(nameof(LookSensetivity), 0.1f);
        MaxLookSensetivity = PlayerPrefs.GetFloat(nameof(MaxLookSensetivity), 1f);
        AllMusicVolume = PlayerPrefs.GetFloat(nameof(AllMusicVolume), 100f);
        MusicVolume = PlayerPrefs.GetFloat(nameof(MusicVolume), 100f);
        EffectsVolume = PlayerPrefs.GetFloat(nameof(EffectsVolume), 100f);
        MinLookSensetivity = PlayerPrefs.GetFloat(nameof(MinLookSensetivity), 0.01f);
        MinLookSensetivity = 0.01f;
        FovValue = PlayerPrefs.GetFloat(nameof(FovValue), 70f);
        FpsMax.Value = PlayerPrefs.GetInt(nameof(FpsMax), 60);
        VSync = Convert.ToBoolean(PlayerPrefs.GetInt(nameof(VSync), 0));
        DataChanged?.Invoke();
    }
}