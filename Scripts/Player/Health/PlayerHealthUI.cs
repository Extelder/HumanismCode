using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Text _maxHealthValueText;
    [SerializeField] private Text _currentHealthValueText;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _damageImageEffectTime;
    [SerializeField] private float _damageDisableImageEffectTime;
    [SerializeField] private float _healthValueToEnableScreenDamage;
    [SerializeField] private Image _damageImage;

    private float _oneProcentFromMaxHealthValue;
    private float _currentdamageImageEffectTime;
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Awake()
    {
        _oneProcentFromMaxHealthValue = _health.MaxValue / 100f;
        _maxHealthValueText.text = _health.MaxValue.ToString();
        _currentHealthValueText.text = _health.MaxValue.ToString();
        OnHealthValueChange(_health.CurrentValue);
    }

    private void OnEnable()
    {
        _health.CurrentValueChanged += OnHealthValueChange;
        _health.MaxValueChanged += OnHealthMaxValueChange;
        _health.DamageTaken += OnDamageTaken;
        _health.Healed += OnHealed;
    }

    private void OnDisable()
    {
        _health.CurrentValueChanged -= OnHealthValueChange;
        _health.MaxValueChanged -= OnHealthMaxValueChange;
        _health.DamageTaken -= OnDamageTaken;
        _health.Healed -= OnHealed;
        _disposable.Clear();
    }

    private void OnHealthValueChange(float value)
    {
        _healthBar.fillAmount = value / _oneProcentFromMaxHealthValue / 100;
        _currentHealthValueText.text = MathF.Round(value, 1).ToString();
    }

    private void OnHealthMaxValueChange(float value)
    {
        _health.MaxValue = value;
        _oneProcentFromMaxHealthValue = _health.MaxValue / 100f;
        _maxHealthValueText.text = _health.MaxValue.ToString();
        _currentHealthValueText.text = _health.MaxValue.ToString();
        OnHealthValueChange(_health.CurrentValue);
    }

    private void OnDamageTaken()
    {
        if (_health.CurrentValue <= _healthValueToEnableScreenDamage)
        {
            _disposable.Clear();
            _damageImage.color = new Color(_damageImage.color.r, _damageImage.color.g, _damageImage.color.b, 0.08f);
        }
        else
        {
            _disposable.Clear();
            LerpDashImageAlpha(0.05f);
        }
    }

    private void OnHealed()
    {
        LerpDashImageAlpha(0);
    }

    private void LerpDashImageAlpha(float needAlpha)
    {
        _currentdamageImageEffectTime = _damageImageEffectTime;
        Observable.EveryUpdate().Subscribe(_ =>
        {
            float a = Mathf.Lerp(_damageImage.color.a, needAlpha, _currentdamageImageEffectTime * Time.deltaTime);
            _damageImage.color = new Color(_damageImage.color.r, _damageImage.color.g, _damageImage.color.b, a);
            if (_damageImage.color.a >= needAlpha - 0.05f)
            {
                needAlpha = 0;
                _currentdamageImageEffectTime = _damageDisableImageEffectTime;
            }
        }).AddTo(_disposable);
    }
}