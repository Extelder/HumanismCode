using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyRandomizeSpeed : MonoBehaviour
{
    [SerializeField] private float _min;
    [SerializeField] private float _max;
    [SerializeField] private NavMeshAgent _agent;

    private void Awake()
    {
        _agent.speed = Random.Range(_min, _max);
    }
}