using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCrack : MonoBehaviour
{
    [SerializeField] private float _alphaClearTime;
    [SerializeField] private float _groundCrackDamageRadius;
    [SerializeField] private float _groundCrackDamage;
    [SerializeField] private LayerMask _groundCrackLayerMask;

    private SpriteRenderer _spriteRenderer;
    private float alpha = 1;
    private Collider[] _colliders;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        alpha = 1;

        Invoke(nameof(CrackGround), 0.3f);
    }

    private void CrackGround()
    {
        _colliders = new Collider[25];
        Physics.OverlapSphereNonAlloc(transform.position, _groundCrackDamageRadius, _colliders, _groundCrackLayerMask);
        foreach (var other in _colliders)
        {
            if (other == null)
                continue;
            Debug.Log(other.gameObject);
            if (other.TryGetComponent<Health>(out Health enemyHealth))
            {
                if (enemyHealth is EnemyHealth health)
                {
                    health.TakeDamage(_groundCrackDamage, 100);
                    continue;
                }

                enemyHealth.TakeDamage(_groundCrackDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _groundCrackDamageRadius);
    }

    private void Update()
    {
        alpha = Mathf.Lerp(alpha, 0, _alphaClearTime * Time.deltaTime);
        Color color = new Color(1, 1, 1, alpha);
        _spriteRenderer.color = color;
    }
}