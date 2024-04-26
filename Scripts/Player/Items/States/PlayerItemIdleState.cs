using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemIdleState : ItemAnimationState
{
    public override void Enter()
    {
        Animator.DisableAllAnimations();
    }
}
