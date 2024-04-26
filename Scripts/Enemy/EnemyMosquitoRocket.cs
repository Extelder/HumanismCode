using System;
using System.Collections;
using System.Collections.Generic;
using RayFire;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMosquitoRocket : PoolObject
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private FollowedObject _followedObject;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private RayfireBomb _rayfireBomb;
    [SerializeField] private UnityEvent _onReturnParticle;
    [SerializeField] private UnityEvent _onEnable;
    [SerializeField] private float _rocketForce;
    [SerializeField] private float _rocketDamage;

    private Collider collider;

    [Header("DetectPlayerOverlappSphere")] [SerializeField]
    private float _radius;

    [SerializeField] private LayerMask _checkLayerMask;
    [SerializeField] private float _checkRate = 0.1f;
    [SerializeField] private float _returnToPoolDelayAfterCollide = 0.3f;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public GameObject IgnoreObject;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(CheckingPlayer());
        _meshRenderer.enabled = true;
        transform.LookAt(_followedObject.transform.position);
        _onEnable?.Invoke();

        Observable.EveryUpdate().Subscribe(_ =>
        {
            _rigidbody.velocity += transform.forward * _rocketForce * Time.deltaTime;
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _disposable.Clear();
        _rigidbody.velocity = new Vector3(0, 0, 0);
    }

    private IEnumerator CheckingPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkRate);
            Collider[] maxColliders = new Collider[7];
            int detectedColliders =
                Physics.OverlapSphereNonAlloc(transform.position, _radius, maxColliders, _checkLayerMask);


            for (int i = 0; i < detectedColliders; i++)
            {
                if (maxColliders[i] == collider)
                    continue;
                if (maxColliders[i].TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
                {
                    playerHealth.TakeDamage(_rocketDamage);
                    ReturnParticle();
                    yield break;
                }
            }

            for (int i = 0; i < detectedColliders; i++)
            {
                if (maxColliders[i].gameObject == IgnoreObject)
                    continue;

                if (maxColliders[i] == collider)
                    continue;

                if (maxColliders[i].TryGetComponent<RayfireRigid>(out RayfireRigid rayfireRigid))
                {
                    rayfireRigid.ApplyDamage(100, transform.position, _radius);
                    ReturnParticle();
                    yield break;
                }

                ReturnParticle();
            }
        }
    }

    private void ReturnParticle()
    {
        _particle.Play();
        _disposable.Clear();
        _rigidbody.velocity = new Vector3(0, 0, 0);
        StopAllCoroutines();
        _meshRenderer.enabled = false;
        _onReturnParticle?.Invoke();
        Invoke(nameof(ReturnToPool), _returnToPoolDelayAfterCollide);
    }
}