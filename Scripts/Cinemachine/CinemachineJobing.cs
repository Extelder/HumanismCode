using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineJobing : DefaultAnimator
{
    [SerializeField] private string _jobbingAnimatorBoolName;
    [SerializeField] private PlayerPhysics _playerPhysics;
    [SerializeField] private AudioSource _stepSource;

    private void Update()
    {
        if (Mathf.Abs(_playerPhysics.Velocity.x) + Mathf.Abs(_playerPhysics.Velocity.z) > 0)
        {
            SetAnimatorBool(_jobbingAnimatorBoolName, true);
        }
        else
        {
            SetAnimatorBool(_jobbingAnimatorBoolName, false);
        }
    }

    public void PlayStepSound()
    {
        if (_playerPhysics.IsGrounded())
            _stepSource?.Play();
    }

    private void OnDisable()
    {
        SetAnimatorBool(_jobbingAnimatorBoolName, false);
    }

    public override void DisableAllAnimations()
    {
        Animator.SetBool(_jobbingAnimatorBoolName, false);
    }
}