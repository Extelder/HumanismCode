using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

public class SmoothLookAt : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lookAtSpeed;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(_target.position - transform.position), _lookAtSpeed * Time.deltaTime);
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}