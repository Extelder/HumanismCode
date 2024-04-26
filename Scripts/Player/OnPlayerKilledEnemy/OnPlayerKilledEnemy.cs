using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerKilledEnemy : MonoBehaviour
{
    [SerializeField] private PlayerHealth _health;

    public static OnPlayerKilledEnemy Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Debug.LogError("Already have OnPlayerKilled");
    }

    public void EnemyKilled(float healValue)
    {
        _health.Heal(healValue);
    }
}