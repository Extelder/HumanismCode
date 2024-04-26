using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloowingPlayerMovementSpeedTrigger : MonoBehaviour
{
    [SerializeField] private float _speedMultiplyer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.SpeedMultiplier = _speedMultiplyer;
            Debug.Log("Detected Player");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.SpeedMultiplier = playerMovement.DefaultMultiplier;
            Debug.Log("Lost Player");
        }
    }
}