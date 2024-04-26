using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FpsLocker : MonoBehaviour
{
    [SerializeField] private DataSettings _settings;

    private CompositeDisposable _disposables = new CompositeDisposable();

    private void OnEnable()
    {
        _settings.FpsMax.Subscribe(_ => { LockFps(); }).AddTo(_disposables);
    }

    private void OnDisable()
    {
        _disposables.Clear();
    }

    private void LockFps()
    {
        Application.targetFrameRate = _settings.FpsMax.Value;
    }
}