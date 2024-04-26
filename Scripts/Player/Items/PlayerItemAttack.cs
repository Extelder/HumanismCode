using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerItemAttack : MonoBehaviour
{
    [SerializeField] private PlayerItemStateMachine _stateMachine;
    [SerializeField] private PlayerItemAnimator _animator;
    [SerializeField] private string _attackVariantIntName;

    private PlayerControls _controls;

    private void OnEnable()
    {
        _controls = new PlayerControls();
        _controls.Enable();

        _controls.Main.ItemAttack.performed += context => StartAttack();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    public void StartAttack()
    {
        if (_stateMachine.CurrentState.CanChangeState == true)
        {
            _animator.SetAnimatorInt(_attackVariantIntName, Random.Range(0, 2));
            _stateMachine.Use();
        }
    }

    public void AttackAnimationEnd()
    {
        _stateMachine.CurrentState.CanChangeState = true;
    }
}