using System.Collections;
using System.Collections.Generic;
using DissolveExample;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealerHealth : Health
{
    [SerializeField] private DissolveChilds _dissolveChilds;

    public UnityEvent Death;

    private void Awake()
    {
        _dissolveChilds = GetComponent<DissolveChilds>();
    }

    public override void HealthBelowOrEqualsZero()
    {
        if (Dead)
            return;
        SpeedUpPlayerAfterEnemyDeath.Instance?.OnEnemyDeath();
        _dissolveChilds.Dissolve(() => { gameObject.SetActive(false); });
        Death?.Invoke();
        base.HealthBelowOrEqualsZero();
    }
}