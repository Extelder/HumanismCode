using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnemyHealthLesserEqualsThan : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private float _needValue;
    [SerializeField] private bool _checkOneTime;
    [SerializeField] private UnityEvent _event;

    private void OnEnable()
    {
        _enemyHealth.CurrentValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        _enemyHealth.CurrentValueChanged -= OnHealthValueChanged;
    }

    private void OnHealthValueChanged(float value)
    {
        if (value <= _needValue)
        {
            _event.Invoke();
            if (_checkOneTime)
                enabled = false;
        }
    }
}
