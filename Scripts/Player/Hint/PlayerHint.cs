using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class PlayerHint : MonoBehaviour
{
    [SerializeField] private DataSettings _dataSettings;
    [SerializeField] private Transform _onMenuOpenedTransform;
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _newTaskAnimationBoolName;
    [SerializeField] private TextMeshProUGUI _textMeshProUgui;

    private string _currentRusText;
    private string _currentEngText;
    private Vector3 _startPosition;
    private CompositeDisposable _disposable = new CompositeDisposable();

    public static PlayerHint Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Debug.LogError("PlayerHint already have");
        Destroy(this);
    }

    private void OnEnable()
    {
        _startPosition = transform.position;
        _dataSettings.SelectedLanguage.Subscribe(_ => { AnimationChangeCurrentText(); }).AddTo(_disposable);
        _playerSettings.Opened.Subscribe(_ =>
        {
            if (_)
            {
                transform.position = _onMenuOpenedTransform.position;
            }
            else
            {
                transform.position = _startPosition;
            }
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    public void ChangeHint(string rusHint, string engHit)
    {
        _animator.SetBool(_newTaskAnimationBoolName, true);
        _currentRusText = rusHint;
        _currentEngText = engHit;
    }

    public void AnimationChangeCurrentText()
    {
        if (_dataSettings.SelectedLanguage.Value == Language.Russian)
            _textMeshProUgui.text = _currentRusText;
        else
            _textMeshProUgui.text = _currentEngText;
    }

    public void AnimationEnd()
    {
        if (_currentEngText == _textMeshProUgui.text || _currentRusText == _textMeshProUgui.text)
        {
            _animator.SetBool(_newTaskAnimationBoolName, false);
        }
    }
}