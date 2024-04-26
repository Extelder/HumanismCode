using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskShootState : WeaponState
{
    [SerializeField] private FlaskShoot _shoot;

    public override void Enter()
    {
        WeaponAnimator.Shoot();
        _shoot.Shoot();
        CanChangeState = false;
    }

    public override void Exit()
    {
        _shoot.StopShooting();
    }

    public void FlaskShootAnimationEnd()
    {
        CanChangeState = true;
    }
}