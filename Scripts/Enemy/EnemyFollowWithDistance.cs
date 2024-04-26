using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EnemyFollowWithDistance : EnemyFollow
{
    [SerializeField] private float _checkDistanceRate;
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    [field: SerializeField] public float Distance { get; private set; }

    public FollowedObject Target;

    private void OnEnable()
    {
        StartFollowing();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }

    public void StartFollowing()
    {
        StopAllCoroutines();
        StartCoroutine(CheckingDistance());
    }

    private IEnumerator CheckingDistance()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_checkDistanceRate);
            if (Vector3.SqrMagnitude(Agent.transform.position - Target.transform.position) > Distance)
            {
                _stateMachine.Idle();
                Agent.isStopped = true;
            }
            else
            {
                FollowDestinationByOneCheck(Target.transform.position);
                _stateMachine.Chase();
            }
        }
    }
}