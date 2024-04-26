using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class DistantEnemyPlayerDetect : EnemyFollow
{
    [SerializeField] private FollowedObject _followedObject;
    [SerializeField] private float _randomPountRadius;
    [SerializeField] private float _generatePointMaxRate;
    [SerializeField] private float _generatePointMinRate;
    [SerializeField] private Transform _patrolZoneCenter;
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private LayerMask _toIgnore;

    private NavMeshPath _navMeshPath;

    private RaycastHit _hit;
    private Vector3 _randomPointPosition;
    private bool getCorrectPoint = false;

    public bool Detected { get; private set; }

    [field: SerializeField] public float Distance { get; private set; }


    private void Awake()
    {
        _navMeshPath = new NavMeshPath();
    }

    private void OnEnable()
    {
        StartCoroutine(GoingRandomPoint());

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Vector3.SqrMagnitude(Agent.transform.position - Agent.pathEndPosition) <= Distance)
            {
                _enemyAnimator.Idle();
            }
            else
            {
                FollowDestinationByOneCheck(_followedObject.transform.position);
                _enemyStateMachine.Chase();
            }

            if (Physics.Linecast(transform.position, _followedObject.transform.position, out _hit, ~_toIgnore))
            {
                if (_hit.collider != null && _hit.collider.TryGetComponent<PlayerHealth>(out PlayerHealth health))
                {
                    Detected = true;
                    _enemyStateMachine.Attack();
                    return;
                }
            }

            Detected = false;
        }).AddTo(Disposable);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }

    private IEnumerator GoingRandomPoint()
    {
        while (true)
        {
            StartCoroutine(RandomNavMeshPoint());
            yield return new WaitUntil(() => getCorrectPoint);
            yield return new WaitForSecondsRealtime(Random.Range(_generatePointMinRate, _generatePointMaxRate));
        }
    }

    private IEnumerator RandomNavMeshPoint()
    {
        getCorrectPoint = false;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(Random.insideUnitSphere * _randomPountRadius + _patrolZoneCenter.position,
            out navMeshHit, _randomPountRadius, NavMesh.AllAreas);
        _randomPointPosition = navMeshHit.position;

        Agent.CalculatePath(_randomPointPosition, _navMeshPath);
        getCorrectPoint = true;

        if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            FollowDestinationByOneCheck(_randomPointPosition);
        }

        yield return new WaitForSeconds(0.03f);
        _enemyStateMachine.Chase();
    }
}