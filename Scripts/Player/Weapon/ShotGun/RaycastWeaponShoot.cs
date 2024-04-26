using UnityEngine;
using System;
using System.Collections;
using NaughtyAttributes;
using Random = UnityEngine.Random;
using RayFire;


public class RaycastWeaponShoot : WeaponShoot
{
    [SerializeField] private Transform _rayFireTarget;
    [SerializeField] private WeaponAmmo _weaponAmmo;
    [SerializeField] private RayfireGun _rayFireGun;
    [SerializeField] private bool _useOwnPools;

    [ShowIf(nameof(_useOwnPools))] [SerializeField]
    private Pool _geometryPool;

    [ShowIf(nameof(_useOwnPools))] [SerializeField]
    private Pool _enemyHitPool;

    [SerializeField] public DefaultWeaponSettings defaultWeaponSettings;
    [SerializeField] private Transform _muzzle;

    [SerializeField] private WeaponTrail _trailRenderer;

    private Vector3 offset;

    public override void Shoot()
    {
        Shooted?.Invoke();

        StartCoroutine(PerformShoot());
    }

    private void Start()
    {
        GetPool();
    }

    private void GetPool()
    {
        if (_useOwnPools)
            return;
        _geometryPool = Pools.Instance.BulletsPool;
        _enemyHitPool = Pools.Instance.EnemyDamageEffectPool;
    }

    private IEnumerator PerformShoot()
    {
        Vector3 cameraPosition = Camera.position;
        Vector3 cameraForward = Camera.forward;
        Quaternion cameraRotation = Camera.rotation;

        for (int i = 0; i < defaultWeaponSettings.AmountOfShot; i++)
        {
            offset = Random.insideUnitCircle * defaultWeaponSettings.Sprey;

            RaycastHit hit =
                GetRaycastHitWithOffsetAndStartEndPoints(offset, cameraPosition, cameraForward, cameraRotation);

            if (hit.collider == null)
                continue;

            SpawnTrail();

            if (hit.collider.TryGetComponent<RayfireRigid>(out RayfireRigid rayfireRigid))
            {
                _rayFireTarget.position = hit.point;
                _rayFireGun.Shoot();
            }

            if (hit.collider.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor weaponVisitor))
            {
                Accept(weaponVisitor, hit);
            }

            yield return new WaitForSecondsRealtime
                (0.005f);
        }

        _weaponAmmo?.TrySpendAmmo(defaultWeaponSettings.SpendAmmoByShoot);
    }

    public override void Accept(IWeaponVisitor visitor, RaycastHit hit)
    {
    }

    public void ReturnGeometryDecal(RaycastHit hit)
    {
        if (defaultWeaponSettings.ReturnDecal)
        {
            PoolObject poolObject = _geometryPool.GetFreeElement(hit.point, Quaternion.identity, null);
            poolObject.transform.rotation = Quaternion.FromToRotation(poolObject.transform.up, hit.normal);
        }
    }

    public void ReturnEnemyHitDecal(RaycastHit hit)
    {
        if (defaultWeaponSettings.ReturnDecal)
        {
            PoolObject poolObject =
                _enemyHitPool.GetFreeElement(hit.point, Quaternion.identity, null);
            poolObject.transform.rotation = Quaternion.FromToRotation(poolObject.transform.up, hit.normal);
            poolObject.transform.rotation = Quaternion.FromToRotation(poolObject.transform.up, hit.normal);
        }
    }


    public void SpawnTrail()
    {
        if (defaultWeaponSettings.SpawnTrail)
        {
            Ray ray = new Ray();
            ray.origin = Camera.position;
            ray.direction = Camera.forward + Camera.rotation * offset;

            WeaponTrail trail = Instantiate(_trailRenderer, _muzzle.position, Quaternion.identity);

            trail.OnSpawn(ray.GetPoint(Range), 25f);
            Debug.DrawRay(_muzzle.position, ray.GetPoint(Range), Color.red, 4);
        }
    }
}