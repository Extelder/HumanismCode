using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class WeaponStateMachine : MonoBehaviour
{
    [Space(-10)] [Header("Weapon States")] [SerializeField]
    private WeaponState _idleState;

    [SerializeField] private WeaponState _walkState;
    [SerializeField] private WeaponState _runState;
    [SerializeField] private WeaponState _shootState;
    [SerializeField] private WeaponState _drawState;
    [SerializeField] private bool _needAmmoToShoot = true;

    [ShowIf(nameof(_needAmmoToShoot))] [Space(10)] [SerializeField]
    private WeaponAmmo _overheatAmmo;

    [ShowIf(nameof(_needAmmoToShoot))] [Space(10)] [SerializeField]
    private WeaponAmmo _ammo;

    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = new StateMachine();
        StateMachine.Init(_idleState);
    }

    private void OnEnable()
    {
        Draw();
    }

    private void OnDisable()
    {
        StateMachine.CurrentState.CanChangeState = true;
        Idle();
    }

    public void Idle()
    {
        StateMachine.ChangeState(_idleState);
    }

    public void Walk()
    {
        StateMachine.ChangeState(_walkState);
    }

    public void Run()
    {
        StateMachine.ChangeState(_runState);
    }

    public void Draw()
    {
        StateMachine.ChangeState(_drawState);
    }

    public void Shoot()
    {
        if (!_needAmmoToShoot)
        {
            StateMachine.ChangeState(_shootState);
            return;
        }

        if (_overheatAmmo.CanShoot() && _ammo.CanShoot() && Time.timeScale >= 1)
        {
            StateMachine.ChangeState(_shootState);
        }
    }

    public void Reload()
    {
    }
}