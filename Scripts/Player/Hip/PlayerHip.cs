using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHip : MonoBehaviour
{
    [SerializeField] private GameObject _defaultHip;

    private GameObject _currentHip;

    private bool _canChangeHip;

    private void Awake()
    {
        _currentHip = _defaultHip;
        StartCoroutine(WaitForStartGame());
    }


    public void ChangeHip(GameObject hip)
    {
        if (!_canChangeHip)
            return;
        if (hip != _currentHip)
        {
            _currentHip?.SetActive(false);
            _currentHip = hip;
            _currentHip.SetActive(true);
        }
    }

    private IEnumerator WaitForStartGame()
    {
        yield return new WaitForSeconds(3);
        _canChangeHip = true;
    }

    public void SetDefaultHip()
    {
        ChangeHip(_defaultHip);
    }
}