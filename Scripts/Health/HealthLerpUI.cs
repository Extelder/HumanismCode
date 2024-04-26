using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HealthLerpUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _lerpSpeed;

    private float _currentHealthValue;
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        _health.CurrentValueChanged += OnHealthValueChanged;
        _health.MaxValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        _health.CurrentValueChanged -= OnHealthValueChanged;
        _health.MaxValueChanged -= OnHealthValueChanged;
    }

    private void OnHealthValueChanged(float value)
    {
        _currentHealthValue = value / 100;

        _disposable.Clear();
        Observable.EveryLateUpdate().Subscribe(_ =>
        {
            _healthBar.fillAmount =
                Mathf.Lerp(_healthBar.fillAmount, _currentHealthValue, _lerpSpeed * Time.deltaTime);
            if (_healthBar.fillAmount == _currentHealthValue)
                _disposable.Clear();
        }).AddTo(_disposable);
    }
    
    private void OnHealthMaxChanged(float value)
    {
        _currentHealthValue = value / 100;
    }
}