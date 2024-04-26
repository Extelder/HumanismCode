using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponOverheatAmmo : WeaponAmmo
{
    [SerializeField] private WeaponStateMachine _weaponStateMachine;
    [SerializeField] private float _recoilRate;
    [SerializeField] private int _ammoByRate;

    private CancellationTokenSource _token = new CancellationTokenSource();

    private void Awake()
    {
        Recoiling();
    }

    private void OnEnable()
    {
        CurrentAmmoValueChanged += OnAmmoValueChanged;
    }

    private void OnDisable()
    {
        CurrentAmmoValueChanged -= OnAmmoValueChanged;
    }

    private void OnDestroy()
    {
        _token.Cancel(false);
    }

    public void OnAmmoValueChanged(int value)
    {
        if (value <= 0)
        {
            _weaponStateMachine.StateMachine.CurrentState.CanChangeState = true;
            _weaponStateMachine.Reload();
        }
    }

    private async void Recoiling()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(_recoilRate), _token.Token);
            AddAmmo(_ammoByRate);
        }
    }

    public override bool CanShoot()
    {
        return CurrentAmmo - _ammoByRate > 0;
    }
}