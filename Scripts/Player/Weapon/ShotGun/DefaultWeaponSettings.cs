using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 2, menuName = "Weapon", fileName = "DefaultWeaponSettings")]
public class DefaultWeaponSettings : WeaponSettings
{
    public float Sprey = 70f;
    public int AmountOfShot;
    public int SpendAmmoByShoot = 1;
    public int DamagePerAmountOfShot;
    public float BarrelExplosiveDistance;
}