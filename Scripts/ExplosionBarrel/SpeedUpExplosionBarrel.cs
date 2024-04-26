using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SpeedUpExplosionBarrel : Health, IWeaponVisitor
{
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;
    [SerializeField] private float _healValue;
    [SerializeField] private float _speedUpValue;
    [SerializeField] private float _explosionRange;
    [SerializeField] private LayerMask _explosionLayerMask;
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
        Collider[] colliders = new Collider[15];
        Physics.OverlapSphereNonAlloc(transform.position, _explosionRange, colliders, _explosionLayerMask);

        foreach (var other in colliders)
        {
            if (other == null)
                continue;
            if (other.TryGetComponent<Health>(out Health health))
            {
                if (health != this)
                {
                    health.Heal(_healValue);
                }
            }

            if (other.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
            {
                movement.TemporarilySpeedUpMovement(_speedUpValue);
                break;
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