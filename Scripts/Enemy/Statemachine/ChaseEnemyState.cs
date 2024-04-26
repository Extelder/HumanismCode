using UnityEngine;


public class ChaseEnemyState : EnemyState
{
    [SerializeField] private EnemyFollow _follow;

    public override void Enter()
    {
        _follow.enabled = true;
        EnemyAnimator.Chase();
    }

    public override void Exit()
    {
        _follow.enabled = false;
    }
}