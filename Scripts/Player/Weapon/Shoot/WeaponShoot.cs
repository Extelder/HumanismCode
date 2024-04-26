using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponShoot : RaycastBehaviour
{
    public Action Shooted;
    public abstract void Shoot();

    public virtual void Accept(IWeaponVisitor visitor, RaycastHit hit)
    {
    }
}