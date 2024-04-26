using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemWalkState : ItemAnimationState
{
    public override void Enter()
    {
        Animator.Walk();
    }
}