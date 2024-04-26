using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageAndTeteportToPositionTrigger : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _damage;
    [SerializeField] private bool _autoDestroyAfterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(_damage);
            playerHealth.transform.position = _transform.position;
            if (_autoDestroyAfterTrigger)
                Destroy(gameObject);
        }
    }
}