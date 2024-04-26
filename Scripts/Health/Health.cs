using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float CurrentValue;
    public float MaxValue;
    public bool Dead;
    public bool DamageBlocked;
    public float DamageMultiply = 1;

    public event Action<float> CurrentValueChanged;
    public event Action<float> MaxValueChanged;
    public event Action Healed;
    public event Action DamageTaken;


    public virtual void TakeDamage(float value)
    {
        if (DamageBlocked)
            return;
        if (CurrentValue - value * DamageMultiply <= 0)
        {
            CurrentValue = 0;
            HealthBelowOrEqualsZero();
            CurrentValueChanged?.Invoke(CurrentValue);
            DamageTaken?.Invoke();
            return;
        }

        CurrentValue -= value * DamageMultiply;
        CurrentValueChanged?.Invoke(CurrentValue);
        DamageTaken?.Invoke();
    }

    public void Heal(float value)
    {
        Healed?.Invoke();
        if (CurrentValue + value > MaxValue)
        {
            CurrentValue = MaxValue;
            CurrentValueChanged?.Invoke(CurrentValue);
            return;
        }

        CurrentValue += value;
        CurrentValueChanged?.Invoke(CurrentValue);
    }

    public void HealToMax()
    {
        CurrentValue = MaxValue;
        CurrentValueChanged?.Invoke(CurrentValue);
    }

    public virtual void HealthBelowOrEqualsZero()
    {
        CurrentValue = 0;
        Dead = true;
    }

    public void MaxValueUpdate()
    {
        MaxValueChanged?.Invoke(MaxValue);
    }
}