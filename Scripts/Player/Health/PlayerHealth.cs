using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void HealthBelowOrEqualsZero()
    {
        base.HealthBelowOrEqualsZero();
    }

    public void SetMaxHealthValue(float value)
    {
        MaxValue = value;
        MaxValueUpdate();
        HealToMax();
    }
}