using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultHitBox : MonoBehaviour, IWeaponVisitor
{
    public void Visit(SuperShotgunShoot SuperShotgunShoot, RaycastHit hit)
    {
        SuperShotgunShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(ShotgunShoot ShotgunShoot, RaycastHit hit)
    {
        ShotgunShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(RifleShoot RifleShoot, RaycastHit hit)
    {
        RifleShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(MinigunOverheatShoot minigunOverheatShoot, RaycastHit hit)
    {
        minigunOverheatShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(MinigunWaterShoot minigunWaterShoot, RaycastHit hit)
    {
        minigunWaterShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(CrossbowShoot crossbowShoot, RaycastHit hit)
    {
        crossbowShoot.ReturnGeometryDecal(hit);
    }

    public void Visit(Projectile WeaponProjectile)
    {
    }

    public void Visit(GravityGunInteractable gravityGunInteractable)
    {
        gravityGunInteractable.ReturnGeometryDecal();
    }

    public void Visit(FiregunZone firegunShoot)
    {
    }
}