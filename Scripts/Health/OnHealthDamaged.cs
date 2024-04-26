using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnHealthDamaged : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private UnityEvent _event;

    private void OnEnable()
    {
        _health.DamageTaken += OnHealthDamageTaken;
    }

    private void OnDisable()
    {
        _health.DamageTaken -= OnHealthDamageTaken;
    }

    private void OnHealthDamageTaken()
    {
        _event.Invoke();
    }
}