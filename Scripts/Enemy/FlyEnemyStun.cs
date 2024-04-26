using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyStun : EnemyStun
{
    [SerializeField] private FlyEnemyFollow _flyEnemyFollow;

    public override void Hooked()
    {
        _flyEnemyFollow.enabled = false;
        Stunned = true;
    }

    public override void Stun()
    {
    }
}