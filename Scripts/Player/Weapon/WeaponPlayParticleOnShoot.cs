using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayParticleOnShoot : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private WeaponShoot _weaponShoot;

    private void OnEnable()
    {
        _weaponShoot.Shooted += OnShoot;
    }

    private void OnDisable()
    {
        _weaponShoot.Shooted -= OnShoot;
    }

    private void OnShoot()
    {
        _particleSystem.Play();
    }
}