using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : DefaultAnimator
{
    [SerializeField] private string _walkAnimationStateBool;
    [SerializeField] private string _runAnimationStateBool;
    [SerializeField] private string _shootAnimationStateBool;

    private void OnDisable()
    {
        DisableAllAnimations();
    }

    public void Idle()
    {
        DisableAllAnimations();
    }

    public void Walk()
    {
        SetAnimatorBoolAndDisableOthers(_walkAnimationStateBool, true);
    }

    public void Run()
    {
        SetAnimatorBoolAndDisableOthers(_runAnimationStateBool, true);
    }

    public void Shoot()
    {
        SetAnimatorBoolAndDisableOthers(_shootAnimationStateBool, true);
    }

    public override void DisableAllAnimations()
    {
        SetAnimatorBool(_walkAnimationStateBool, false);
        SetAnimatorBool(_runAnimationStateBool, false);
        SetAnimatorBool(_shootAnimationStateBool, false);
    }
}