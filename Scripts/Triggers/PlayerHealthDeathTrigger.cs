using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.HealthBelowOrEqualsZero();
        }
    }
}