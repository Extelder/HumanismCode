using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }

    public void ChangeState(State state)
    {
        if (CurrentState.CanChangeState && CurrentState != state)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }
    }

    public virtual void Init(State startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }
}