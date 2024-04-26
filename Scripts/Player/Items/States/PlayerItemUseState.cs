using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUseState : ItemAnimationState
{
    public override void Enter()
    {
        CanChangeState = false;
        Animator.Use();
    }

    public void AnimationEnd()
    {
        CanChangeState = true;
    }
}