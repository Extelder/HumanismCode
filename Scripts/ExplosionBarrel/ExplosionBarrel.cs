using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ExplosionBarrel : Health, IWeaponVisitor
{
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;
    [SerializeField] private float _damage;
    [SerializeField] private float _explosionRange;
    [SerializeField] private ParticleSystem _explosiveParticle;
    [SerializeField] private GameObject _destroyedBarrel;
    [SerializeField] private GameObject _notDestroyedBarrel;

    private bool _explosived;

    private void Awake()
    {
        HealToMax();
    }

    public void DefaultVisit(RaycastWeaponShoot raycastWeaponWeaponShoot, RaycastHit hit)
    {
        raycastWeaponWeaponShoot.ReturnGeometryDecal(hit);

        TakeDamage(raycastWeaponWeaponShoot.defaultWeaponSettings.DamagePerAmountOfShot);

        if (hit.distance <= raycastWeaponWeaponShoot.defaultWeaponSettings.BarrelExplosiveDistance)
            Explosive();
    }

    public void Visit(SuperShotgunShoot SuperShotgunShoot, RaycastHit hit)
    {
        DefaultVisit(SuperShotgunShoot, hit);
    }

    public void Visit(ShotgunShoot ShotgunShoot, RaycastHit hit)
    {
        DefaultVisit(ShotgunShoot, hit);
    }

    public void Visit(RifleShoot RifleShoot, RaycastHit hit)
    {
        DefaultVisit(RifleShoot, hit);
    }

    public void Visit(MinigunOverheatShoot minigunOverheatShoot, RaycastHit hit)
    {
        DefaultVisit(minigunOverheatShoot, hit);
    }

    public void Visit(MinigunWaterShoot minigunWaterShoot, RaycastHit hit)
    {
        DefaultVisit(minigunWaterShoot, hit);
    }

    public void Visit(CrossbowShoot crossbowShoot, RaycastHit hit)
    {
        DefaultVisit(crossbowShoot, hit);
    }

    public void Visit(Projectile WeaponProjectile)
    {
        TakeDamage(WeaponProjectile.Damage);
    }

    public void Visit(GravityGunInteractable gravityGunInteractable)
    {
        TakeDamage(gravityGunInteractable.Damage);
        gravityGunInteractable.ReturnGeometryDecal();
    }

    public void Visit(FiregunZone firegunShoot)
    {
        TakeDamage(firegunShoot.Damage);
    }

    public void Explosive()
    {
        if (_explosived)
            return;
        _cinemachineImpulseSource.GenerateImpulse();
        _explosived = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRange);

        foreach (var other in colliders)
        {
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_damage, transform.position, _explosionRange);
            }

            if (other.TryGetComponent<Health>(out Health health))
            {
                if (health != this)
                    health.TakeDamage(_damage / Vector3.SqrMagnitude(transform.position - health.transform.position));
            }
        }

        _notDestroyedBarrel.SetActive(false);
        _destroyedBarrel.SetActive(true);
        _explosiveParticle?.Play();
        GetComponent<Collider>().enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }

    public override void HealthBelowOrEqualsZero()
    {
        Explosive();
    }
}