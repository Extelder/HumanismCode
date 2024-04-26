using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : DefaultAnimator
{
    [SerializeField] private string _chaseAnimationBool = "IsChasing";
    [SerializeField] private string _isStunAnimationBool = "IsStunned";
    [SerializeField] private string _isAttackingAnimationBool = "IsAttacking";

    public void Idle()
    {
        DisableAllAnimations();
    }

    public void Chase()
    {
        SetAnimatorBoolAndDisableOthers(_chaseAnimationBool, true);
    }

    public void Attack()
    {
        SetAnimatorBoolAndDisableOthers(_isAttackingAnimationBool, true);
    }

    public void TakeDamage()
    {
        SetAnimatorBoolAndDisableOthers(_isStunAnimationBool, true);
    }

    public override void DisableAllAnimations()
    {
        SetAnimatorBool(_chaseAnimationBool, false);
        SetAnimatorBool(_isStunAnimationBool, false);
        SetAnimatorBool(_isAttackingAnimationBool, false);
    }
}