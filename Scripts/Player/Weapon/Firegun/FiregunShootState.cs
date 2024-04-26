using System;
using UnityEngine;


public class FiregunShootState : WeaponState
{
    [SerializeField] private WeaponInput _weaponInput;
    [SerializeField] private FiregunShoot _shoot;


    public override void Enter()
    {
        CanChangeState = false;

        _weaponInput.Controls.Main.Shoot.canceled += context => CanChangeState = true;
        WeaponAnimator.Shoot();
        _shoot.Shoot();
    }

    private void OnDisable()
    {
        _weaponInput.Controls.Main.Shoot.canceled -= context => CanChangeState = true;
    }

    public override void Exit()
    {
        _weaponInput.Controls.Main.Shoot.canceled -= context => CanChangeState = true;
        _shoot.StopShoot();
    }
}