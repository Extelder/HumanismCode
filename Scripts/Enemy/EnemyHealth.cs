using System;
using System.Collections;
using System.Collections.Generic;
using DissolveExample;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class EnemyHealth : Health
{
    [SerializeField] private DissolveChilds _dissolveChilds;
    [SerializeField] private ParticleSystem _onEnemyHealingParticleSystem;
    [SerializeField] private EnemyStun _enemyStun;
    [field: SerializeField] public float PlayerHealValue { get; private set; } = 20f;

    private Random _random;

    public UnityEvent Death;

    private void OnEnable()
    {
        Healed += OnHealed;
    }

    private void OnDisable()
    {
        Healed -= OnHealed;
    }

    private void OnHealed()
    {
        _onEnemyHealingParticleSystem.Play();
    }

    private void Awake()
    {
        _dissolveChilds = GetComponent<DissolveChilds>();
    }

    public void TakeDamage(float value, float stunChance)
    {
        TakeDamage(value);
        _random = new Random();
        double random = _random.NextDouble();
        if (random < stunChance)
        {
            _enemyStun.Stun();
        }
    }

    public override void HealthBelowOrEqualsZero()
    {
        if (Dead)
            return;
        SpeedUpPlayerAfterEnemyDeath.Instance?.OnEnemyDeath();
        AddAmmoToWeaponsAfterEnemyDeath.Instance?.OnEnemyKilled();
        _dissolveChilds.Dissolve(() => { gameObject.SetActive(false); });
        Death?.Invoke();
        base.HealthBelowOrEqualsZero();
    }
}