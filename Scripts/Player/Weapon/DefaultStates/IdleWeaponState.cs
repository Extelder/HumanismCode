using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleWeaponState : WeaponState
{
    public override void Enter()
    {
        WeaponAnimator.Idle();
    }
}