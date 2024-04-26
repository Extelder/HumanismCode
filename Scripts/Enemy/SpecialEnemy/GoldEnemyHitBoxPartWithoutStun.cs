using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEnemyHitBoxPartWithoutStun : MonoBehaviour, IWeaponVisitor
{
    [SerializeField] private Health _health;
    [SerializeField] private MeshRenderer _goldMaterialRenderer;

    [SerializeField] private float _intensity;

    private Material _goldMaterial;

    private void Awake()
    {
        _goldMaterial = _goldMaterialRenderer.material;
    }

    public void Visit(SuperShotgunShoot SuperShotgunShoot, RaycastHit hit)
    {
    }

    public void Visit(ShotgunShoot ShotgunShoot, RaycastHit hit)
    {
    }

    public void Visit(RifleShoot RifleShoot, RaycastHit hit)
    {
    }

    public void Visit(MinigunOverheatShoot minigunOverheatShoot, RaycastHit hit)
    {
        Default(minigunOverheatShoot);
        minigunOverheatShoot.ReturnGeometryDecal(hit);
        _intensity += 0.3f;
        _goldMaterial.SetVector("_EmissionColor", _goldMaterial.color * _intensity);
    }

    public void Visit(MinigunWaterShoot minigunWaterShoot, RaycastHit hit)
    {
    }

    public void Visit(CrossbowShoot crossbowShoot, RaycastHit hit)
    {
    }

    public void Visit(Projectile WeaponProjectile)
    {
        _health.TakeDamage(WeaponProjectile.Damage);
    }

    public void Visit(GravityGunInteractable gravityGunInteractable)
    {
    }

    public void Visit(FiregunZone firegunShoot)
    {
    }

    public void Default(RaycastWeaponShoot raycastWeaponShoot)
    {
        _health.TakeDamage(raycastWeaponShoot.defaultWeaponSettings.DamagePerAmountOfShot);
    }
}