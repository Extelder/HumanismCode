using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunWeaponState : WeaponState
{
    public override void Enter()
    {
        WeaponAnimator.Run();
    }
}