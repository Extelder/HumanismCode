using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RigdepoleTrigger : MonoBehaviour
{
    [SerializeField] private float _damage;

    public UnityEvent OnTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IRidgepoleDamagable>(out IRidgepoleDamagable ridgepoleDamagable))
        {
            ridgepoleDamagable.Damage(_damage);
            OnTriggered?.Invoke();
        }
    }
}