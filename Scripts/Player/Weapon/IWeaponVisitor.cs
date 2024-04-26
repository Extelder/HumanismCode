using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponVisitor
{
    public void Visit(SuperShotgunShoot SuperShotgunShoot, RaycastHit hit);
    public void Visit(ShotgunShoot ShotgunShoot, RaycastHit hit);
    public void Visit(RifleShoot RifleShoot, RaycastHit hit);
    public void Visit(MinigunOverheatShoot minigunOverheatShoot, RaycastHit hit);
    public void Visit(MinigunWaterShoot minigunWaterShoot, RaycastHit hit);
    public void Visit(CrossbowShoot crossbowShoot, RaycastHit hit);
    public void Visit(Projectile WeaponProjectile);
    public void Visit(GravityGunInteractable gravityGunInteractable);
    public void Visit(FiregunZone firegunShoot);
}