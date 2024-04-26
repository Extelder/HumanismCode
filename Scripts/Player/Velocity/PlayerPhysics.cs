using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerPhysics : MonoBehaviour
{
    private CharacterController _characterController;

    public Vector3 Velocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _characterController.Move(Velocity * Time.deltaTime);
    }

    public bool IsGrounded() => _characterController.isGrounded;
}