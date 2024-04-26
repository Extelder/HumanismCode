using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Rocket,
    GauskaProjectile
}

public class WeaponProjectileShoot : WeaponShoot
{
    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] private WeaponAmmo _weaponAmmo;

    public Pool ProjectilesPool;
    public Transform Muzzle;

    private void Start()
    {
        switch (_projectileType)
        {
            case ProjectileType.Rocket:
                ProjectilesPool = Pools.Instance.RocketsPool;
                break;
            case ProjectileType.GauskaProjectile:
                ProjectilesPool = Pools.Instance.GauskaPool;
                break;
        }
    }


    public override void Shoot()
    {
        Shooted?.Invoke();

        Vector3 direction = Camera.position + Camera.forward * Range;
        Projectile projectile = ProjectilesPool
            .GetFreeElement(Muzzle.position, Quaternion.FromToRotation(Muzzle.position, direction))
            .GetComponent<Projectile>();
        projectile.Initiate(direction);
        _weaponAmmo.TrySpendAmmo(1);
    }
}