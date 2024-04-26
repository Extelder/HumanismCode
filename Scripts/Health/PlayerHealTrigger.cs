using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealTrigger : MonoBehaviour
{
    [SerializeField] private float _healValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            if (playerHealth.MaxValue == playerHealth.CurrentValue)
                return;
            playerHealth.Heal(_healValue);
            Destroy(gameObject);
        }
    }
}