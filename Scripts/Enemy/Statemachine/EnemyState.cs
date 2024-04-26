using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : State
{
    public EnemyAnimator EnemyAnimator;

    public abstract override void Enter();
}