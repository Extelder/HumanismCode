using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyOctoShoot : MonoBehaviour
{
    [SerializeField] private float _minRate;
    [SerializeField] private float _maxRate;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _laserDuration;
    [SerializeField] private GameObject _laser;
    [SerializeField] private Transform _rotationObject;
    [SerializeField] private FollowedObject _target;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CloseEnemyPlayerDetect _closeEnemyPlayerDetect;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        StartCoroutine(Lasering());
    }

    private void OnDisable()
    {
        StopLooking();
        StopAllCoroutines();
        _disposable.Clear();
    }

    private IEnumerator Lasering()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minRate, _maxRate));
            _laser.SetActive(true);
            _audioSource?.Play();
            StartLooking();
            StartCoroutine(WaitForStopLasering());
            yield return new WaitUntil(() => _closeEnemyPlayerDetect.Detected == true || _laser.activeSelf == false);
            StopCoroutine(WaitForStopLasering());
            _laser.SetActive(false);
            StopLooking();
            _audioSource?.Stop();
        }
    }

    private void StartLooking()
    {
        _agent.updateRotation = false;
        Observable.EveryUpdate().Subscribe(_ =>
            {
                _rotationObject.rotation = Quaternion.Slerp(_rotationObject.rotation,
                    Quaternion.LookRotation(_target.transform.position - _rotationObject.position),
                    _rotationSpeed * Time.deltaTime);
            })
            .AddTo(_disposable);
    }

    private void StopLooking()
    {
        _agent.updateRotation = true;
        _disposable.Clear();
        _rotationObject.localEulerAngles = new Vector3(0, 0, 0);
    }

    private IEnumerator WaitForStopLasering()
    {
        yield return new WaitForSeconds(_laserDuration);
        _laser.SetActive(false);
    }
}