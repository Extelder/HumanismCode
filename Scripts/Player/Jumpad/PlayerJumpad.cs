using System;
using System.Collections;
using System.Collections.Generic;
using LincolnCpp.HUDIndicator;
using UniRx;
using UnityEngine;

public class PlayerJumpad : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Pool _jumpadsPool;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _impulse;
    [SerializeField] private int _startAmount;
    [SerializeField] private int _maxAmount;

    private int _currentAmount;
    private PlayerControls _controls;

    public event Action<int> CurrentJumpadsAmountValueChanged;

    private void Awake()
    {
        _currentAmount = _startAmount;
        CurrentJumpadsAmountValueChanged?.Invoke(_currentAmount);
    }

    private void OnEnable()
    {
        _controls = new PlayerControls();
        _controls.Enable();
        _controls.Main.ThrowJumpad.performed += context => ThrowJumpad();
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Main.ThrowJumpad.performed -= context => ThrowJumpad();
    }

    public void ThrowJumpad()
    {
        if (_currentAmount > 0)
        {
            PoolObject poolObject = _jumpadsPool.GetFreeElement(_muzzle.position, Quaternion.Euler(
                Quaternion.identity.x, _camera.localEulerAngles.y,
                Quaternion.identity.z));
            poolObject.OnCreate();
            poolObject.GetComponent<Rigidbody>().AddForce(poolObject.transform.forward * _impulse, ForceMode.Impulse);
            poolObject.GetComponent<Rigidbody>().AddForce(-poolObject.transform.up * _impulse, ForceMode.Impulse);
            _currentAmount--;
            CurrentJumpadsAmountValueChanged?.Invoke(_currentAmount);
        }
    }

    public void OnJumpadPickuped(int amount)
    {
        if (_currentAmount + amount >= _maxAmount)
        {
            _currentAmount = _maxAmount;
            CurrentJumpadsAmountValueChanged?.Invoke(_currentAmount);
            return;
        }

        _currentAmount += amount;
        CurrentJumpadsAmountValueChanged?.Invoke(_currentAmount);
    }
}