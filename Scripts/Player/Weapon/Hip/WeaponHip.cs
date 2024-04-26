using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHip : MonoBehaviour
{
    [SerializeField] private GameObject _hip;
    [SerializeField] private PlayerHip _playerHip;

    private void OnEnable()
    {
        _playerHip.ChangeHip(_hip);
    }

    private void OnDisable()
    {
        _playerHip.SetDefaultHip();
    }
}