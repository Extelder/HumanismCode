using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class LocalizationTextMeshPro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUgui;
    [SerializeField] private string _russianText;
    [SerializeField] private string _englishText;
    [SerializeField] private DataSettings _dataSettings;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        _dataSettings.SelectedLanguage.Subscribe(_ =>
        {
            if (_ == Language.Russian)
            {
                UpdateTextToRussian();
            }
            else
            {
                UpdateTextToEnglish();
            }
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    public void UpdateTextToRussian()
    {
        _textMeshProUgui.text = _russianText;
    }

    public void UpdateTextToEnglish()
    {
        _textMeshProUgui.text = _englishText;
    }
}