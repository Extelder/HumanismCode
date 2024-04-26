using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemRunState : ItemAnimationState
{
    public override void Enter()
    {
        Animator.Run();
    }
}
