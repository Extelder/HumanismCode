using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeaponState : WeaponState
{
    [SerializeField] private bool _needAmmoToShoot = true;

    private WeaponAmmo _weaponAmmo;

    private void Awake()
    {
        if (!_needAmmoToShoot)
            return;
        _weaponAmmo = GetComponent<WeaponAmmo>();
    }

    public override void Enter()
    {
        CanChangeState = false;
        if (!_needAmmoToShoot)
        {
            WeaponAnimator.Shoot();
            return;
        }

        if (_weaponAmmo.CanShoot())
            WeaponAnimator.Shoot();
    }

    public void AnimationStateEnded()
    {
        CanChangeState = true;
    }
}