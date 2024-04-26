using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class LookAtMosquito : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private Transform _rotationObject;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private FollowedObject _target;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        StartLooking();
    }

    private void OnDisable()
    {
        StopLooking();
    }

    private void StartLooking()
    {
        Debug.Log("StartLooking");
        _agent.updateRotation = false;
        Observable.EveryUpdate().Subscribe(_ =>
            {
                _rotationObject.localRotation = Quaternion.Slerp(_rotationObject.localRotation,
                    Quaternion.LookRotation(_target.transform.position - _rotationObject.position),
                    _rotationSpeed * Time.deltaTime);
            })
            .AddTo(_disposable);
    }

    private void StopLooking()
    {
        Debug.Log("StopLooking");
        _agent.updateRotation = true;
        _disposable.Clear();
        _rotationObject.localEulerAngles = new Vector3(0, 0, 0);
    }
}