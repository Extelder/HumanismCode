using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum AxisType
{
    X,
    Y,
    Z
}

public class SetRotationToTargetByAxis : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Transform _target;
    [SerializeField] private AxisType _axisType;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        switch (_axisType)
        {
            case AxisType.X:
                Observable.EveryUpdate().Subscribe(_ =>
                {
                    transform.eulerAngles = new Vector3(_target.eulerAngles.x, transform.eulerAngles.y,
                        transform.eulerAngles.z);
                }).AddTo(_disposable);
                break;
            case AxisType.Y:

                Observable.EveryUpdate().Subscribe(_ =>
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, _target.eulerAngles.y,
                        transform.eulerAngles.z);
                }).AddTo(_disposable);

                break;
            case AxisType.Z:

                Observable.EveryUpdate().Subscribe(_ =>
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
                        _target.eulerAngles.z);
                }).AddTo(_disposable);

                break;
        }
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}