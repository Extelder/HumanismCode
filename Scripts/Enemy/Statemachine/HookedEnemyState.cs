using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HookedEnemyState : EnemyState
{
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private EnemyFollow _enemyFollow;

    public override void Enter()
    {
        _enemyFollow.enabled = false;
        DisableColliders();
        GetComponent<NavMeshAgent>().enabled = false;
        CanChangeState = false;
        EnemyAnimator.TakeDamage();
    }

    public void DisableColliders()
    {
        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = false;
        }
    }
}