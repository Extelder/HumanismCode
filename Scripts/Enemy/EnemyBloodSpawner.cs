using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBloodSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPlace;
    [SerializeField] private Pool _bloodPool;
    [SerializeField] private float _minBloodContainers;
    [SerializeField] private float _maxBloodContainers;

    public void SpawnBloodContainers()
    {
        for (int i = 0; i < Random.Range(_minBloodContainers, _maxBloodContainers); i++)
        {
            Vector3 spawnPosition = _spawnPlace.position + new Vector3(Random.insideUnitSphere.x * 0.4f,
                0, Random.insideUnitSphere.z * 0.4f);
            _bloodPool.GetFreeElement(spawnPosition, Quaternion.identity);
        }
    }
}