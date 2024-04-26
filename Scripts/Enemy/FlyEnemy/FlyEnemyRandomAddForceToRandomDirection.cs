using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyEnemyRandomAddForceToRandomDirection : MonoBehaviour
{
    [SerializeField] private float _minRate;
    [SerializeField] private float _maxRate;
    [SerializeField] private float _minForce;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _movingTime;

    private System.Random _random;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _random = new System.Random();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(AddingRadomForceByRate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator AddingRadomForceByRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minRate, _maxRate));
            int randomDirection = _random.Next(1, 4);
            Vector3 direction = transform.forward;
            float force = Random.Range(_minForce, _maxForce);
            switch (randomDirection)
            {
                case 1:
                    direction = Vector3.forward;
                    break;
                case 2:
                    direction = -Vector3.forward;
                    break;
                case 3:
                    direction = Vector3.right;
                    break;
                case 4:
                    direction = -Vector3.right;
                    break;
            }

            _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
            yield return new WaitForSeconds(_movingTime);
            _rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }
}