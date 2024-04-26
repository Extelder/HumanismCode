using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShoot : RaycastWeaponShoot
{
    public override void Accept(IWeaponVisitor visitor, RaycastHit hit)
    {
        visitor.Visit(this, hit);
    }
}