using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxPart : MonoBehaviour, IWeaponVisitor
{
    [SerializeField] private float _damageMultiply = 1f;
    [SerializeField] private float _shotGunstunChance = 0.07f;
    [SerializeField] private float _superShotGunstunChance = 0.15f;
    [SerializeField] private float _rifleStunChance = 0.003f;
    [SerializeField] private float _crossbowStunChance = 0.25f;
    [SerializeField] private float _projectileStunChance = 0.25f;
    [SerializeField] private float _fireGunStunChance = 0.005f;
    [SerializeField] private EnemyHealth _enemyHealth;

    public void Visit(SuperShotgunShoot SuperShotgunShoot, RaycastHit hit)
    {
        _enemyHealth.TakeDamage(SuperShotgunShoot.defaultWeaponSettings.DamagePerAmountOfShot * _damageMultiply,
            _superShotGunstunChance);
        SuperShotgunShoot.ReturnEnemyHitDecal(hit);
    }

    public void Visit(ShotgunShoot ShotgunShoot, RaycastHit hit)
    {
        _enemyHealth.TakeDamage(ShotgunShoot.defaultWeaponSettings.DamagePerAmountOfShot * _damageMultiply,
            _shotGunstunChance);
        ShotgunShoot.ReturnEnemyHitDecal(hit);
    }

    public void Visit(RifleShoot RifleShoot, RaycastHit hit)
    {
        _enemyHealth.TakeDamage(RifleShoot.defaultWeaponSettings.DamagePerAmountOfShot * _damageMultiply,
            _rifleStunChance);
        RifleShoot.ReturnEnemyHitDecal(hit);
    }

    public void Visit(MinigunOverheatShoot minigunOverheatShoot, RaycastHit hit)
    {
        _enemyHealth.TakeDamage(minigunOverheatShoot.defaultWeaponSettings.DamagePerAmountOfShot * _damageMultiply,
            _rifleStunChance);
        minigunOverheatShoot.ReturnEnemyHitDecal(hit);
    }

    public void Visit(MinigunWaterShoot minigunWaterShoot, RaycastHit hit)
    {
        _enemyHealth.TakeDamage(minigunWaterShoot.defaultWeaponSettings.DamagePerAmountOfShot * _damageMultiply,
            _rifleStunChance);
        minigunWaterShoot.ReturnEnemyHitDecal(hit);
    }

    public void Visit(CrossbowShoot crossbowShoot, RaycastHit hit)
    {
        _enemyHealth.TakeDamage(crossbowShoot.defaultWeaponSettings.DamagePerAmountOfShot * _damageMultiply,
            _crossbowStunChance);
        crossbowShoot.ReturnEnemyHitDecal(hit);
    }

    public void Visit(Projectile WeaponProjectile)
    {
        _enemyHealth.TakeDamage(WeaponProjectile.Damage * _damageMultiply, _projectileStunChance);
    }

    public void Visit(GravityGunInteractable gravityGunInteractable)
    {
        Debug.Log(gravityGunInteractable.Damage);
        _enemyHealth.TakeDamage(gravityGunInteractable.Damage * _damageMultiply, 1);
        gravityGunInteractable.ReturnGeometryDecal();
    }

    public void Visit(FiregunZone firegunShoot)
    {
        _enemyHealth.TakeDamage(firegunShoot.Damage * _damageMultiply, _fireGunStunChance);
    }
}