using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using RayFire;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterExplose : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;
    [SerializeField] private LayerMask _explosionDetectLayerMask;
    [SerializeField] private float _damage;
    [SerializeField] private float _explosionRange;
    [SerializeField] private ParticleSystem _explosiveParticle;

    [field: SerializeField] public bool CanExplosive { get; set; }

    public UnityEvent OnExplosive;

    private void OnTriggerEnter(Collider other)
    {
        if (other != this && CanExplosive)
        {
            Explosive();
        }
    }

    public void Explosive()
    {
        _cinemachineImpulseSource.GenerateImpulse();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRange, _explosionDetectLayerMask);

        foreach (var other in colliders)
        {
            if (other.TryGetComponent<RayfireRigid>(out RayfireRigid rayfireRigid))
            {
                rayfireRigid.ApplyDamage(100, transform.position, 10f);
            }

            if (other.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_damage, transform.position, _explosionRange);
            }

            if (other.TryGetComponent<Health>(out Health health))
            {
                if (health != this)
                    health.TakeDamage(_damage / Vector3.SqrMagnitude(transform.position - health.transform.position));
            }
        }

        _explosiveParticle?.Play();
        OnExplosive?.Invoke();
        GetComponent<Collider>().enabled = false;
    }
}