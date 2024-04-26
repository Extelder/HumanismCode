using System;
using System.Collections;
using System.Collections.Generic;
using RayFire;
using UnityEngine;

public class GravityGunInteractable : MonoBehaviour
{
    [SerializeField] private float _minVelocityToDamage;
    [SerializeField] private float _returnToLayerCheckRate = 0.2f;
    [SerializeField] private float _checkPlayerRadius = 3f;
    [SerializeField] private LayerMask _checkPlayerLayerMask;
    [SerializeField] private Outline _outline;
    [SerializeField] private Pool _damageEffectPool;
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public bool Pushed { get; set; }
    [field: SerializeField] public bool CanHandl { get; set; } = true;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void StartHandling()
    {
        StopAllCoroutines();
        gameObject.layer = LayerMask.NameToLayer("Projectile");
        _outline.OutlineColor = Color.red;
    }

    public virtual void UpdateHandling()
    {
    }

    public virtual void Impulsed()
    {
    }

    public virtual void StopHandling()
    {
        StartCoroutine(ReturnDefaultLayer());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _checkPlayerRadius);
    }

    private IEnumerator ReturnDefaultLayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(_returnToLayerCheckRate);

            Collider[] colliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(transform.position, _checkPlayerRadius, colliders,
                _checkPlayerLayerMask);
            if (colliders[0] != null)
            {
                gameObject.layer = LayerMask.NameToLayer("Projectile");
                _outline.OutlineColor = Color.red;
                Debug.Log("Collider null");
                continue;
            }

            gameObject.layer = LayerMask.NameToLayer("Default");
            _outline.OutlineColor = Color.cyan;
            yield break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Rigidbody.velocity.sqrMagnitude >= _minVelocityToDamage)
        {
            if (other.collider.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
            {
                visitor.Visit(this);
            }

            if (other.collider.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(Damage);
            }

            if (other.collider.TryGetComponent<RayfireRigid>(out RayfireRigid rayfireRigid))
            {
                rayfireRigid.ApplyDamage(100, transform.position, 10f);
            }
        }
    }

    public void ReturnGeometryDecal()
    {
        _damageEffectPool.GetFreeElement(transform.position, Quaternion.identity, null);
    }
}