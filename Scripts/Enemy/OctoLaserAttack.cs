using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class OctoLaserAttack : MonoBehaviour
{
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackRate;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage;

    private void OnEnable()
    {
        StartCoroutine(CheсkingPlayerHealth());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator CheсkingPlayerHealth()
    {
        while (true)
        {
            Collider[] collider = new Collider[15];
            Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, collider, _layerMask);
            foreach (var other in collider)
            {
                if (other == null)
                    continue;
                if (other.TryGetComponent<PlayerHealth>(out PlayerHealth health))
                {
                    health.TakeDamage(_damage);
                }
            }

            yield return new WaitForSeconds(_attackRate);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}