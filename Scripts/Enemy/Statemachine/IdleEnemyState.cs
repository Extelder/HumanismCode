using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class IdleEnemyState : EnemyState
{
    [SerializeField] private FollowedObject _followedObject;
    [SerializeField] private NavMeshAgent _agent;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public override void Enter()
    {
        _agent.isStopped = true;
        EnemyAnimator.Idle();
        Observable.EveryUpdate().Subscribe(_ =>
            {
                transform.LookAt(_followedObject.transform.position, transform.up);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            })
            .AddTo(_disposable);
    }

    public override void Exit()
    {
        _agent.isStopped = false;
        _disposable.Clear();
    }
}