using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class AttackEnemyState : EnemyState
{
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private FollowedObject _followedObject;

    [SerializeField] private NavMeshAgent _agent;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnDisable()
    {
        _disposable.Clear();
    }

    public override void Enter()
    {
        _disposable.Clear();
        _agent.isStopped = true;
        CanChangeState = false;
        EnemyAnimator.Attack();
        Observable.EveryUpdate().Subscribe(_ =>
            {
                _agent.transform.LookAt(_followedObject.transform.position, _agent.transform.up);
                _agent.transform.eulerAngles = new Vector3(0, _agent.transform.eulerAngles.y, 0);
            })
            .AddTo(_disposable);
    }

    public override void Exit()
    {
        _disposable.Clear();
        _agent.isStopped = false;
    }

    public void AttackAnimationEnd()
    {
        CanChangeState = true;
        _enemyStateMachine.Chase();
    }
}