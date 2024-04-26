using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealerOverlap : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _healRate;
    [SerializeField] private float _healPerRate;
    [SerializeField] private LayerMask _healLayerMask;

    private Collider[] _other = new Collider[15];

    private void OnEnable()
    {
        StartCoroutine(HealingOverlapped());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator HealingOverlapped()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_healPerRate);
            _other = new Collider[10];
            Physics.OverlapSphereNonAlloc(transform.position, _radius, _other, _healLayerMask);
            foreach (var other in _other)
            {
                if (other == null)
                    continue;
                if (other.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
                    enemyHealth.Heal(_healPerRate);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}