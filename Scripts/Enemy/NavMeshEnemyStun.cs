using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshEnemyStun : EnemyStun
{
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private EnemyFollow _enemyFollow;


    public override void Hooked()
    {
        Stunned = true;
        _enemyStateMachine.Hooked();
    }

    public override void Stun()
    {
        Stunned = true;
        _enemyFollow.enabled = false;
        _enemyStateMachine.TakeDamage();
    }
}