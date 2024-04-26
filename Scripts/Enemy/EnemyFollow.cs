using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    public CompositeDisposable Disposable { get; private set; } = new CompositeDisposable();

    public virtual void OnDisable()
    {
        Disposable.Clear();
        StopAllCoroutines();
        StopFollowing();
    }

    public void StopFollowing()
    {
        Disposable.Clear();
        Agent.isStopped = true;
    }

    public void FollowDestinationByOneCheck(Vector3 position)
    {
        Agent.isStopped = false;
        Agent.SetDestination(position);
    }

    public void FollowDestinationWithAlwaysCheck(Transform targetTransform)
    {
        Disposable.Clear();
        Observable.EveryUpdate().Subscribe(_ =>
            {
                if (targetTransform != null) FollowDestinationByOneCheck(targetTransform.position);
            })
            .AddTo(Disposable);
    }
}