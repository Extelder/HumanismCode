using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAnimationState : State
{
    public PlayerDefaultAnimator Animator;

    public abstract override void Enter();
}
