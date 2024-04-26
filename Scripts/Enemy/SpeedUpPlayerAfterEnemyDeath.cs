using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPlayerAfterEnemyDeath : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    public static SpeedUpPlayerAfterEnemyDeath Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Destroy(this);
    }

    public void OnEnemyDeath()
    {
        _playerMovement.TemporarilySpeedUpMovement(10);
    }
}