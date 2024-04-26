using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmoChecker : MonoBehaviour
{
    private WeaponAmmo _weaponAmmo;
    private WeaponStateMachine _weaponStateMachine;

    private void Awake()
    {
        _weaponAmmo = GetComponent<WeaponAmmo>();
        _weaponStateMachine = GetComponent<WeaponStateMachine>();
    }

    private void OnEnable()
    {
        _weaponAmmo.CurrentAmmoValueChanged += OnAmmoValueChanged;
    }

    private void OnDisable()
    {
        _weaponAmmo.CurrentAmmoValueChanged -= OnAmmoValueChanged;
    }

    private void OnAmmoValueChanged(int value)
    {
        if (value == 0)
        {
            _weaponStateMachine.StateMachine.CurrentState.CanChangeState = true;
            _weaponStateMachine.Idle();
        }
    }
}