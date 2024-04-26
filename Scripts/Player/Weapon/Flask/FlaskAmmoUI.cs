using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaskAmmoUI : MonoBehaviour
{
    [SerializeField] private Text _ammoValueText;
    [SerializeField] private Text _oxygenValueText;
    [SerializeField] private FlaskAmmo _ammo;

    private void Awake()
    {
        OnFlaskAmmoValueChanged(_ammo.CurrentAmmo);
        OnFlaskOxygenValueChanged(_ammo.CurrentOxygen);
    }

    private void OnEnable()
    {
        _ammo.CurrentAmmoValueChanged += OnFlaskAmmoValueChanged;
        _ammo.CurrentOxygenValueChanged += OnFlaskOxygenValueChanged;
    }

    private void OnDisable()
    {
        _ammo.CurrentAmmoValueChanged -= OnFlaskAmmoValueChanged;
        _ammo.CurrentOxygenValueChanged -= OnFlaskOxygenValueChanged;
    }

    private void OnFlaskAmmoValueChanged(int value)
    {
        _ammoValueText.text = value.ToString() + " L.";
    }

    private void OnFlaskOxygenValueChanged(int value)
    {
        _oxygenValueText.text = value.ToString() + " %";
    }
}