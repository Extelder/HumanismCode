using System.Collections;
using UniRx;
using UnityEngine;

public class TakeDamageEnemyState : EnemyState
{
    [SerializeField] private EnemyStun navMeshEnemyStun;
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private float _stunTime;

    public override void Enter()
    {
        CanChangeState = false;
        StartCoroutine(WaitForDeStun());
        EnemyAnimator.TakeDamage();
    }

    private IEnumerator WaitForDeStun()
    {
        yield return new WaitForSecondsRealtime(_stunTime);
        CanChangeState = true;
        _enemyStateMachine.Chase();
        navMeshEnemyStun.Stunned = false;
    }
}