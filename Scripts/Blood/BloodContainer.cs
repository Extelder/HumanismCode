using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BloodContainer : MonoBehaviour
{
    public ReactiveProperty<int> CurrentBloodAmount = new ReactiveProperty<int>();

    public int OxygenInProcents;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        CurrentBloodAmount.Subscribe(_ =>
        {
            if (_ <= 0)
                gameObject.SetActive(false);
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}