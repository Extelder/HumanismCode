using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyFollow : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FollowedObject _target;
    [SerializeField] private float _speed = 25;
    [SerializeField] private float _detectionDistance = 100;
    [SerializeField] private float _raycastOffset = 2.5f;
    [SerializeField] private float _rotationalDamp = 125;

    private void Update()
    {
        Move();
        Pathfinging();
    }

    private void Move()
    {
        _rigidbody.velocity += transform.forward * _speed * Time.deltaTime;
    }

    private void Turn()
    {
        Vector3 position = _target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationalDamp * Time.deltaTime);
    }

    private void Pathfinging()
    {
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right * _raycastOffset;
        Vector3 right = transform.position + transform.right * _raycastOffset;
        Vector3 up = transform.position + transform.up * _raycastOffset;
        Vector3 down = transform.position - transform.up * _raycastOffset;

        Debug.DrawRay(left, transform.forward * _detectionDistance, Color.cyan);
        Debug.DrawRay(right, transform.forward * _detectionDistance, Color.cyan);
        Debug.DrawRay(up, transform.forward * _detectionDistance, Color.cyan);
        Debug.DrawRay(down, transform.forward * _detectionDistance, Color.cyan);

        if (Physics.Raycast(right, transform.forward, out hit, _detectionDistance))
            raycastOffset += Vector3.right;
        else if (Physics.Raycast(left, transform.forward, out hit, _detectionDistance))
            raycastOffset -= Vector3.right;

        if (Physics.Raycast(up, transform.forward, out hit, _detectionDistance))
            raycastOffset -= Vector3.up;
        else if (Physics.Raycast(down, transform.forward, out hit, _detectionDistance))
            raycastOffset += Vector3.down;

        if (raycastOffset != Vector3.zero)
        {
            transform.Rotate(raycastOffset * 200f * Time.deltaTime);
        }
        else
        {
            Turn();
        }
    }
}