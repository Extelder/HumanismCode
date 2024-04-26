using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerItemStateMachine : PlayerDefaultStateMachine
{
    [SerializeField] private State _walkState;
    [SerializeField] private State _runState;
    [SerializeField] private State _jumpState;
    [SerializeField] private State _attackState;
    [SerializeField] private State _idleState;

    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private PlayerRun _run;

    private CompositeDisposable _disposable = new CompositeDisposable();
    private PlayerControls _controls;


    private void OnEnable()
    {
        Init(_idleState);

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (IsInAir())
            {
                Jump();
                return;
            }

            if (Mathf.Abs(_physics.Velocity.x) + Mathf.Abs(_physics.Velocity.z) > 0)
            {
                if (_run.Running())
                {
                    Run();
                    return;
                }

                Walk();
                return;
            }


            Idle();
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    public override void Idle()
    {
        ChangeState(_idleState);
    }

    public override void Walk()
    {
        ChangeState(_walkState);
    }

    public override void Run()
    {
        ChangeState(_runState);
    }

    public override void Jump()
    {
        ChangeState(_jumpState);
    }

    public override void Use()
    {
        ChangeState(_attackState);
    }

    public bool IsInAir() => !_physics.IsGrounded();
}