using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState _idleEnemyState;
    [SerializeField] private EnemyState _chaseEnemyState;
    [SerializeField] private EnemyState _attackEnemyState;
    [SerializeField] private EnemyState _takeDamageEnemyState;
    [SerializeField] private EnemyState _hookedEnemyState;

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        _stateMachine.Init(_idleEnemyState);
    }

    public void Idle()
    {
        _stateMachine.ChangeState(_idleEnemyState);
    }

    public void Chase()
    {
        _stateMachine.ChangeState(_chaseEnemyState);
    }

    public void Attack()
    {
        _stateMachine.ChangeState(_attackEnemyState);
    }

    public void TakeDamage()
    {
        _stateMachine.CurrentState.CanChangeState = true;
        _stateMachine.ChangeState(_takeDamageEnemyState);
    }

    public void Hooked()
    {
        _stateMachine.CurrentState.CanChangeState = true;
        _stateMachine.ChangeState(_hookedEnemyState);
    }
}