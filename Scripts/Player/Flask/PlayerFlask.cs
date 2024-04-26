using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlask : MonoBehaviour
{
    [SerializeField] private FlaskPlacer _flaskPlacer;
    [SerializeField] private FlaskShoot _flaskShoot;
    [SerializeField] private FlaskAmmo _flaskAmmo;

    private bool _canPlaced;

    private void OnEnable()
    {
        _flaskAmmo.CurrentAmmoValueChanged += OnFlaskAmmoValueChanged;
    }

    private void OnDisable()
    {
        _flaskAmmo.CurrentAmmoValueChanged -= OnFlaskAmmoValueChanged;
        _flaskPlacer.FlaskPlaced -= OnFlaskPlaced;
    }

    private void OnFlaskAmmoValueChanged(int value)
    {
        if (value == _flaskAmmo.MaxAmmo)
        {
            OnFlaskFilled();
        }
    }

    public void DisableFlaskPlacer()
    {
        _flaskPlacer.enabled = false;
    }

    public void EnableFlaskPlacer()
    {
        if (_canPlaced)
        {
            _flaskPlacer.enabled = true;
        }
    }

    private void OnFlaskFilled()
    {
        _flaskShoot.enabled = false;
        _flaskPlacer.enabled = true;
        _canPlaced = true;
        _flaskPlacer.FlaskPlaced += OnFlaskPlaced;
    }

    private void OnFlaskPlaced(PlacedFlask placedFlask)
    {
        _flaskPlacer.FlaskPlaced -= OnFlaskPlaced;
        _flaskShoot.enabled = true;
        _flaskPlacer.enabled = false;
        _canPlaced = false;
    }
}