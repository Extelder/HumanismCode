using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWall : MonoBehaviour
{
    [SerializeField] private LineRenderer[] _lineRenderers;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void EnableLasers()
    {
        for (int i = 0; i < _lineRenderers.Length; i++)
        {
            _lineRenderers[i].enabled = true;
        }

        _collider.enabled = true;
    }

    public void DisableLasers()
    {
        for (int i = 0; i < _lineRenderers.Length; i++)
        {
            _lineRenderers[i].enabled = false;
        }

        _collider.enabled = false;
    }
}