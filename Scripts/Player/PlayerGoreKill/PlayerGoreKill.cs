using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoreKill : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private CinemachineRotation _cinemachineRotation;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _killAnimationTriggerName = "Kill";

    private EnemyHealth _enemyHealth;
    private Action _onKilled;

    public void Kill(EnemyHealth enemyHealth, Action onKilled)
    {
        _enemyHealth = enemyHealth;
        _onKilled = onKilled;

        _cinemachineRotation.DisableRotation();
        _camera.transform.LookAt(enemyHealth.transform.position);

        SetAnimatorTrigger();
    }

    public void KillAnimationTrigger()
    {
        OnPlayerKilledEnemy.Instance.EnemyKilled(_enemyHealth.PlayerHealValue);
        _cinemachineRotation.EnableRotation();
        _enemyHealth.HealthBelowOrEqualsZero();
        _onKilled?.Invoke();
        _cinemachineRotation.enabled = true;
    }

    private void SetAnimatorTrigger()
    {
        _animator.SetTrigger(_killAnimationTriggerName);
    }
}