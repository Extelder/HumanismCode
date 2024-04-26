using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHealthValue : MonoBehaviour
{
    [SerializeField] private UpgradeHealthValueLevel[] _upgradeLevels;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private FlaskAmmo _flaskAmmo;

    private UpgradeHealthValueLevel _currentUpgradeLevel;

    private void Awake()
    {
        for (int i = 0; i < _upgradeLevels.Length; i++)
        {
            if (_upgradeLevels[i].Index == PlayerPrefs.GetInt("HealthUpgradeLevel", 0))
            {
                _currentUpgradeLevel = _upgradeLevels[i];
                UpdateHealthValue();
            }
        }
    }

    public void Upgrade()
    {
        UpgradeHealthValueLevel levelToBuy = null;
        for (int i = 0; i < _upgradeLevels.Length; i++)
        {
            if (_upgradeLevels[i].Index == (_currentUpgradeLevel.Index + 1))
            {
                levelToBuy = _currentUpgradeLevel;
            }
        }

        if (levelToBuy == null)
        {
            return;
        }
    }

    private void UpdateHealthValue()
    {
        _playerHealth.SetMaxHealthValue(_currentUpgradeLevel.MaxHealthValue);
    }
}