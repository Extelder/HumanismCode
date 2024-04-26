using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerHookVisualize : MonoBehaviour
{
    [SerializeField] private float _throwHookTime;
    [SerializeField] private LineRenderer _hookRenderer;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnDisable()
    {
        _disposable.Clear();
    }

    public void ThrowHook(Vector3 position, Action onHookReachedTarget)
    {
        _hookRenderer.enabled = true;
        _hookRenderer.useWorldSpace = true;
        _hookRenderer.SetPosition(0, transform.position);
        Vector3 TargetPosition = transform.position;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            TargetPosition = Vector3.Lerp(TargetPosition, position, _throwHookTime * Time.deltaTime);
            _hookRenderer.SetPosition(1, TargetPosition);
            if (Vector3.SqrMagnitude(TargetPosition - position) <= 1f)
            {
                Debug.Log("hookReached");
                _disposable.Clear();
                onHookReachedTarget?.Invoke();
            }
        }).AddTo(_disposable);
    }

    public void BreackHook()
    {
        _hookRenderer.enabled = false;
        _disposable.Clear();
    }

    public void BacktrackHook(Vector3 position)
    {
        _hookRenderer.useWorldSpace = true;
        _hookRenderer.SetPosition(0, transform.position);
        Vector3 TargetPosition = transform.position;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            TargetPosition = Vector3.Lerp(TargetPosition, position, _throwHookTime * Time.deltaTime);
            _hookRenderer.SetPosition(1, TargetPosition);
        }).AddTo(_disposable);
    }
}