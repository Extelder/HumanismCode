using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(WeaponAmmo))]
public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentAmmoText;

    private WeaponAmmo _weaponAmmo;

    private void Awake()
    {
        _weaponAmmo = GetComponent<WeaponAmmo>();
        _weaponAmmo.CurrentAmmoValueChanged += OnAmmoValueChanged;
    }

    private void OnEnable()
    {
        _currentAmmoText.enabled = true;
        OnAmmoValueChanged(_weaponAmmo.CurrentAmmo);
    }

    private void OnDisable()
    {
        _currentAmmoText.enabled = false;
    }

    private void OnDestroy()
    {
        _weaponAmmo.CurrentAmmoValueChanged -= OnAmmoValueChanged;
    }

    private void OnAmmoValueChanged(int value)
    {
        _currentAmmoText.text = value.ToString();
    }
}