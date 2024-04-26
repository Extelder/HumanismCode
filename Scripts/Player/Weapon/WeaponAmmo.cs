using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int CurrentAmmo;
    public int MaxAmmo;

    public Action<int> CurrentAmmoValueChanged;
    public Action<int> MaxAmmoValueChanged;

    public bool TrySpendAmmo(int value)
    {
        if (CurrentAmmo - value >= 0)
        {
            CurrentAmmo -= value;
            CurrentAmmoValueChanged?.Invoke(CurrentAmmo);

            return true;
        }

        if (CurrentAmmo - value <= 0)
        {
            CurrentAmmo = 0;
            CurrentAmmoValueChanged?.Invoke(CurrentAmmo);
        }

        return false;
    }

    public void SetMaxAmmoValue(int value)
    {
        if (value <= 0)
            return;

        MaxAmmo = value;
        MaxAmmoValueChanged?.Invoke(MaxAmmo);
    }

    public void AddAmmo(int value)
    {
        if (CurrentAmmo + value >= MaxAmmo)
        {
            CurrentAmmo = MaxAmmo;
            CurrentAmmoValueChanged?.Invoke(CurrentAmmo);
            return;
        }

        CurrentAmmo += value;
        CurrentAmmoValueChanged?.Invoke(CurrentAmmo);
    }

    public virtual bool CanShoot()
        => CurrentAmmo > 0;
}