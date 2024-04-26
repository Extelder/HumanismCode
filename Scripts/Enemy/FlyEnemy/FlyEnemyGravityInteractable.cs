using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyEnemyGravityInteractable : GravityGunInteractable
{
    [SerializeField] private EnemyShootByRate _enemyShootByRate;
    [SerializeField] private OnTriggerEnterExplose _onTriggerEnterExplose;
    [SerializeField] private SmoothLookAt _smoothLookAt;
    [SerializeField] private UnityEvent OnStartHandling;
    [SerializeField] private UnityEvent OnStopHandling;

    public override void StartHandling()
    {
        _enemyShootByRate.enabled = false;
        _smoothLookAt.enabled = false;
        OnStartHandling?.Invoke();
        base.StartHandling();
    }

    public override void StopHandling()
    {
        _enemyShootByRate.enabled = true;
        _smoothLookAt.enabled = true;
        OnStopHandling?.Invoke();
        base.StopHandling();
    }

    public override void Impulsed()
    {
        _onTriggerEnterExplose.CanExplosive = true;
        _enemyShootByRate.enabled = false;
    }
}