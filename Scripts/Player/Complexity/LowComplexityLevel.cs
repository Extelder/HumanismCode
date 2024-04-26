using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowComplexityLevel : ComplexityLevel
{
    [SerializeField] private PlayerHealth _playerHealth;

    public override void LevelSelected()
    {
        _playerHealth.DamageMultiply = 0.3f;
    }
}
