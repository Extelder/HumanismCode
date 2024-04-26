using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UniRx;
using UnityEngine;

public class CinemachineFOV : MonoBehaviour
{
    [SerializeField] private float _FOVValueChangeSmoothly;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    public float DefaultFov;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Start()
    {
        _cinemachineVirtualCamera.m_Lens.FieldOfView = DefaultFov;
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    public void SetFOVSmoothly(float value, bool mineSmooth, float smoothly = 0.1f)
    {
        float smooth = _FOVValueChangeSmoothly;

        if (mineSmooth)
        {
            smooth = smoothly;
        }

        _disposable.Clear();
        Observable.EveryUpdate().Subscribe(_ =>
        {
            _cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_cinemachineVirtualCamera.m_Lens.FieldOfView,
                value, smooth * Time.deltaTime);
        }).AddTo(_disposable);
    }

    public void SetDefaulSmoothly()
    {
        SetFOVSmoothly(DefaultFov, false);
    }

    public void SetFOV(float value)
    {
        _cinemachineVirtualCamera.m_Lens.FieldOfView = value;
    }
}