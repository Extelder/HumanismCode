using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisactivateWithDelay : MonoBehaviour
{
    [SerializeField] private float _delay;

    private void Start()
    {
        Invoke(nameof(Disable), _delay);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}