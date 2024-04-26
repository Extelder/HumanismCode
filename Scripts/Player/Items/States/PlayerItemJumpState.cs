using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerItemJumpState : ItemAnimationState
{
    [SerializeField] private PlayerPhysics _physics;
    private CompositeDisposable _disposable = new CompositeDisposable();

    public override void Enter()
    {
        Animator.Jump();
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_physics.IsGrounded())
            {
                Animator.NotInAir();
                _disposable.Clear();
            }
            else
            {
                Animator.InAir();
            }
        }).AddTo(_disposable);
    }

    public override void Exit()
    {
        _disposable.Clear();
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}