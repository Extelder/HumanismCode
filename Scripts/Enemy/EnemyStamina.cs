using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStamina : MonoBehaviour
{
    [SerializeField] private float _staminaReturnTimeInSeconds;
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _minStamina;
    [SerializeField] private NavMeshEnemyStun navMeshEnemyStun;

    [SerializeField] private float _currentStamina;
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Awake()
    {
        _maxStamina = Random.Range(_minStamina, _maxStamina);
        _currentStamina = _maxStamina;
    }

    private void OnEnable()
    {
        _currentStamina = _maxStamina;
        StartSpendingStamina();
    }

    private void StartSpendingStamina()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_currentStamina <= 0)
            {
                _currentStamina = 0;
                navMeshEnemyStun.Stun();
                StartCoroutine(ResumeStamina());
                _disposable.Clear();
                return;
            }

            _currentStamina -= 2f * Time.deltaTime;
        }).AddTo(_disposable);
    }

    private IEnumerator ResumeStamina()
    {
        yield return new WaitForSecondsRealtime(_staminaReturnTimeInSeconds);
        _currentStamina = _maxStamina;
        StartSpendingStamina();
    }

    private void OnDisable()
    {
        _disposable.Clear();
        StopAllCoroutines();
    }
}