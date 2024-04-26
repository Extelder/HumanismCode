using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpenDoorTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
        {
            _door.Open();
        }

        if (other.TryGetComponent<EnemyStateMachine>(out EnemyStateMachine enemyStateMachine))
        {
            _door.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
        {
            _door.Close();
        }

        if (other.TryGetComponent<EnemyStateMachine>(out EnemyStateMachine enemyStateMachine))
        {
            _door.Close();
        }
    }
}