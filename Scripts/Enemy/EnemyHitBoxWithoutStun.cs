using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxWithoutStun : MonoBehaviour, IWeaponVisitor
{
    [SerializeField] private Health _health;

    public void Visit(SuperShotgunShoot SuperShotgunShoot, RaycastHit hit)
    {
        Default(SuperShotgunShoot);
        SuperShotgunShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(ShotgunShoot ShotgunShoot, RaycastHit hit)
    {
        Default(ShotgunShoot);
        ShotgunShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(RifleShoot RifleShoot, RaycastHit hit)
    {
        Default(RifleShoot);
        RifleShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(MinigunOverheatShoot minigunOverheatShoot, RaycastHit hit)
    {
        Default(minigunOverheatShoot);
        minigunOverheatShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(MinigunWaterShoot minigunWaterShoot, RaycastHit hit)
    {
        Default(minigunWaterShoot);
        minigunWaterShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(CrossbowShoot crossbowShoot, RaycastHit hit)
    {
        Default(crossbowShoot);
        crossbowShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(Projectile WeaponProjectile)
    {
        _health.TakeDamage(WeaponProjectile.Damage);
    }

    public void Visit(GravityGunInteractable gravityGunInteractable)
    {
        _health.TakeDamage(gravityGunInteractable.Damage);
        gravityGunInteractable.ReturnGeometryDecal();
    }

    public void Visit(FiregunZone firegunShoot)
    {
        _health.TakeDamage(firegunShoot.Damage);
    }

    public void Default(RaycastWeaponShoot raycastWeaponShoot)
    {
        _health.TakeDamage(raycastWeaponShoot.defaultWeaponSettings.DamagePerAmountOfShot);
    }
}