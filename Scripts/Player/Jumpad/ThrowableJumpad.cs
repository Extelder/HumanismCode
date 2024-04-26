using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableJumpad : PoolObject
{
    [SerializeField] private float _jumpadForce;

    private bool _canJumpad;

    private void OnEnable()
    {
    }

    public override void OnCreate()
    {
        Invoke(nameof(Can), 1f);
    }

    private void Can()
    {
        _canJumpad = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canJumpad == false) return;
        if (other.TryGetComponent<PlayerJump>(out PlayerJump playerPhysics))
        {
            playerPhysics.JumpWithOther(_jumpadForce);
            _canJumpad = false;
        }
    }
}