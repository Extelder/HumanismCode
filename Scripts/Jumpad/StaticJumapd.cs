using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaticJumapd : MonoBehaviour
{
    [SerializeField] private float _jumpadForce;

    public UnityEvent Jumped;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerJump>(out PlayerJump playerPhysics))
        {
            playerPhysics.JumpWithOther(_jumpadForce);
            Jumped?.Invoke();
        }
    }
}