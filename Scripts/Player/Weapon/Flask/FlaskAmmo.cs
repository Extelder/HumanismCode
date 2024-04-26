using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskAmmo : WeaponAmmo
{
    public int OxyngenChangeRate = 2;
    public int TargetOxygen;
    public int CurrentOxygen;
    public int MaxOxygen = 100;
    public Action<int> CurrentOxygenValueChanged;

    private void OnEnable()
    {
        StartCoroutine(ChangingCurrentOxygenValueToTargetByOneSecond());
    }

    private IEnumerator ChangingCurrentOxygenValueToTargetByOneSecond()
    {
        while (true)
        {
            yield return new WaitUntil(() => TargetOxygen != CurrentOxygen);
            yield return new WaitForSecondsRealtime(OxyngenChangeRate);
            if (TargetOxygen > CurrentOxygen)
            {
                CurrentOxygen++;
            }
            else
            {
                CurrentOxygen--;
            }

            CurrentOxygenValueChanged?.Invoke(CurrentOxygen);
        }
    }

    public void AddOxygen(int value)
    {
        if (TargetOxygen + value >= MaxOxygen)
        {
            TargetOxygen = MaxOxygen;
            return;
        }

        MaxOxygen += value;
    }

    public void RemoveOxygen(int value)
    {
        if (TargetOxygen - value >= 0)
        {
            TargetOxygen -= value;

            return;
        }

        TargetOxygen = 0;
    }

    public override bool CanShoot()
    {
        return CurrentAmmo < MaxAmmo;
    }
}