using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDefaultStateMachine : StateMachine
{
    public abstract void Idle();
    public abstract void Walk();
    public abstract void Run();
    public abstract void Jump();
    public abstract void Use();
}