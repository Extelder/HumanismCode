using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWeaponState : WeaponState
{
    public override void Enter()
    {
        CanChangeState = false;
    }

    public void DrawAnimationStateEnded()
    {
        CanChangeState = true;
    }
}