using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UglyPotatoEasterEgg : MonoBehaviour, IWeaponVisitor
{
    public UnityEvent Hitted;

    public void Visit(SuperShotgunShoot SuperShotgunShoot, RaycastHit hit)
    {
        Hitted?.Invoke();
    }

    public void Visit(ShotgunShoot ShotgunShoot, RaycastHit hit)
    {
        Hitted?.Invoke();
    }

    public void Visit(RifleShoot RifleShoot, RaycastHit hit)
    {
        Hitted?.Invoke();
    }

    public void Visit(MinigunOverheatShoot minigunOverheatShoot, RaycastHit hit)
    {
        Hitted?.Invoke();
    }

    public void Visit(MinigunWaterShoot minigunWaterShoot, RaycastHit hit)
    {
        Hitted?.Invoke();
    }

    public void Visit(CrossbowShoot crossbowShoot, RaycastHit hit)
    {
        Hitted?.Invoke();
    }

    public void Visit(Projectile WeaponProjectile)
    {
        Hitted?.Invoke();
    }

    public void Visit(GravityGunInteractable gravityGunInteractable)
    {
        Hitted?.Invoke();
    }

    public void Visit(FiregunZone firegunShoot)
    {
        Hitted?.Invoke();
    }
}