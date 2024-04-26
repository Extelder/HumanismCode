using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighComplexityLevel : ComplexityLevel
{
    [SerializeField] private PlayerHealth _playerHealth;

    public override void LevelSelected()
    {
        _playerHealth.DamageMultiply = 1.2f;
    }
}