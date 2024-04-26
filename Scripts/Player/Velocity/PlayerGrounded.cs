using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    public bool OnGround { get; private set; }

    private void Update()
    {
        Debug.Log(OnGround);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerPhysics>(out PlayerPhysics playerPhysics))
            return;
        OnGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerPhysics>(out PlayerPhysics playerPhysics))
            return;
        OnGround = false;
    }
}