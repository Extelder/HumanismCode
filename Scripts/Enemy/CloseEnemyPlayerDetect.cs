using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CloseEnemyPlayerDetect : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackDamage = 20;
    [SerializeField] private float _attackRange;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private PlayerHealth _detectedHealth;

    public bool Detected { get; private set; }

    private void OnEnable()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            Collider[] detectedColliders = Physics.OverlapSphere(_attackPoint.position, _attackRange);
            foreach (Collider other in detectedColliders)
            {
                if (other.TryGetComponent<PlayerHealth>(out PlayerHealth health))
                {
                    Detected = true;
                    _enemyStateMachine.Attack();
                    _detectedHealth = health;
                    return;
                }
            }

            _detectedHealth = null;
            Detected = false;
        }).AddTo(_disposable);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public void Attack()
    {
        if (_detectedHealth == null)
        {
            return;
        }

        _detectedHealth.TakeDamage(_attackDamage);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}