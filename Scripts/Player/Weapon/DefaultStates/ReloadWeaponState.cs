using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeaponState : WeaponState
{
    public override void Enter()
    {
        CanChangeState = false;
    }

    public void ReloadAnimationEnd()
    {
        CanChangeState = true;
    }
}