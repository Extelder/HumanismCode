using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHealth : Health
{
    [SerializeField] private float _damagingRate;
    [SerializeField] private float _damagePerRate;

    public float MaxOxygenPerRate = 20;
    public static HumanHealth Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Debug.LogError("HumanHealth already have");
        Destroy(this);
    }

    private void OnEnable()
    {
        StartCoroutine(Damaging());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Damaging()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_damagingRate);
            TakeDamage(_damagePerRate);
        }
    }
}