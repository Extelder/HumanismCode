using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerItemAnimator : PlayerDefaultAnimator
{
    [SerializeField] private string _walkBoolName;
    [SerializeField] private string _runBoolName;
    [SerializeField] private string _inAirBoolName;
    [SerializeField] private string _attackBoolName;
    [SerializeField] private string _jumpTriggerName;

    public override void Walk()
    {
        SetAnimatorBoolAndDisableOthers(_walkBoolName, true);
    }

    public override void Run()
    {
        SetAnimatorBoolAndDisableOthers(_runBoolName, true);
    }

    public override void Use()
    {
        SetAnimatorBoolAndDisableOthers(_attackBoolName, true);
    }

    public override void Jump()
    {
        SetAnimatorTriggerAndDisableOthers(_jumpTriggerName);
    }

    public override void NotInAir()
    {
        SetAnimatorBoolAndDisableOthers(_inAirBoolName, false);
    }

    public override void InAir()
    {
        SetAnimatorBoolAndDisableOthers(_inAirBoolName, true);
    }

    public override void DisableAllAnimations()
    {
        SetAnimatorBool(_walkBoolName, false);
        SetAnimatorBool(_runBoolName, false);
        SetAnimatorBool(_inAirBoolName, false);
        SetAnimatorBool(_attackBoolName, false);
    }
}