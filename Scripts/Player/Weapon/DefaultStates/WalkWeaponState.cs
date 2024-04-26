using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkWeaponState : WeaponState
{
    public override void Enter()
    {
        WeaponAnimator.Walk();
    }
}
