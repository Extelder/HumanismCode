using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanHealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private HumanHealth _health;

    private void Awake()
    {
        OnHealthValueChanged(_health.CurrentValue);
    }

    private void OnEnable()
    {
        _health.CurrentValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        _health.CurrentValueChanged -= OnHealthValueChanged;
    }

    private void OnHealthValueChanged(float value)
    {
        _healthBar.fillAmount = value / 100;
    }
}