using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthOxygenDecrease : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private FlaskAmmo _flaskAmmo;

    private void OnEnable()
    {
        _playerHealth.DamageTaken += OnDamageTaken;
    }

    private void OnDisable()
    {
        _playerHealth.DamageTaken -= OnDamageTaken;
    }

    private void OnDamageTaken()
    {
        _flaskAmmo.TrySpendAmmo(1);
    }
}