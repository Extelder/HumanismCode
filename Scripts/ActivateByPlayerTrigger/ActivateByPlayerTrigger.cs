using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateByPlayerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;

    public UnityEvent OnActivate;

    private Collider _collider;

    private void Awake()
    {
        for (int i = 0; i < GetComponents<Collider>().Length; i++)
        {
            if (GetComponents<Collider>()[i].isTrigger == true)
            {
                _collider = GetComponents<Collider>()[i];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _collider.enabled = false;
            Activate();
        }
    }

    public virtual void Activate()
    {
        for (int i = 0; i < _gameObjects.Length; i++)
        {
            _gameObjects[i].SetActive(true);
        }

        OnActivate?.Invoke();
    }
}