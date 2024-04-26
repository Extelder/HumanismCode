using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using RayFire;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : PoolObject
{
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;
    [SerializeField] private RayfireBomb _bomb;
    [field: SerializeField] public float Damage { get; private set; }
    [SerializeField] private float _explosionRange;
    [SerializeField] private bool _onlyPlayerHealth;
    [SerializeField] private ParticleSystem _explosiveParticle;
    [SerializeField] private float _speed;

    private Collider[] colliders = new Collider[50];
    private CompositeDisposable _disposable = new CompositeDisposable();
    private bool _explosived;

    public void Initiate(Vector3 targetPosition)
    {
        _disposable?.Clear();
        Observable.EveryLateUpdate().Subscribe(_ =>
        {
            transform.position =
                Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
            if (Vector3.SqrMagnitude(transform.position - targetPosition) <= 2)
            {
                Explode();
            }
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
        _explosived = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (_explosived)
            return;
        _cinemachineImpulseSource?.GenerateImpulse();
        _explosived = true;
        _disposable.Clear();
        Physics.OverlapSphereNonAlloc(transform.position, _explosionRange, colliders);

        foreach (var other in colliders)
        {
            if (!other)
                continue;

            if (other.TryGetComponent<RayfireRigid>(out RayfireRigid rigid))
            {
                _bomb.Explode(0);
            }

            if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
            {
                if (_onlyPlayerHealth)
                    continue;
                Accept(visitor);
            }

            if (other.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(Damage, transform.position, _explosionRange);
            }

            if (other.TryGetComponent<Health>(out Health health))
            {
                if (health is PlayerHealth && _onlyPlayerHealth)
                {
                    health.TakeDamage(Damage / Vector3.SqrMagnitude(transform.position - health.transform.position));
                    continue;
                }

                if (health != this)
                    health.TakeDamage(Damage / Vector3.SqrMagnitude(transform.position - health.transform.position));
            }
        }

        _explosiveParticle?.Play();
        Invoke(nameof(ReturnToPool), 0.5f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }

    public void Accept(IWeaponVisitor visitor)
    {
        visitor.Visit(this);
    }
}