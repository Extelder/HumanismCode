using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedUpFov;
    [SerializeField] private CinemachineFOV _cinemachineFov;
    [SerializeField] private float _returnTemporarilySpeedUpSmooth;
    [SerializeField] private float _wallRunSpeedMultiply;
    [SerializeField] private float _defaultMoveSpeed;
    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private float _currentMoveSpeed;

    public PlayerControls Controls;
    private float _targetMoveSpeed;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public float SpeedMultiplier = 1;
    public float DefaultMultiplier = 1;

    private void Awake()
    {
        Controls = new PlayerControls();
        _currentMoveSpeed = _defaultMoveSpeed;
        _targetMoveSpeed = _defaultMoveSpeed;
    }

    private void OnEnable()
    {
        SpeedMultiplier = DefaultMultiplier;
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
        _disposable.Clear();
    }

    private void FixedUpdate()
    {
        _currentMoveSpeed = _targetMoveSpeed;

        Vector3 moveDirection = transform.TransformDirection(new Vector3(
            Controls.Main.MoveLeftRight.ReadValue<float>() * _currentMoveSpeed * Time.deltaTime,
            0,
            Controls.Main.MoveForwardBackward.ReadValue<float>() * _currentMoveSpeed *
            Time.deltaTime));

        _physics.Velocity.x = moveDirection.x * SpeedMultiplier;
        _physics.Velocity.z = moveDirection.z * SpeedMultiplier;
    }

    public void ChangeSpeed(float value)
    {
        _targetMoveSpeed = value;
    }

    public void ChangeSpeedToWallRunning()
    {
        _targetMoveSpeed *= _wallRunSpeedMultiply;
    }

    public void ChangeSpeedToDefault()
    {
        _targetMoveSpeed /= _wallRunSpeedMultiply;
    }

    public void TemporarilySpeedUpMovement(float value)
    {
        _targetMoveSpeed += value;

        if (_disposable.Count <= 0)
        {

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (_targetMoveSpeed - 1 > _defaultMoveSpeed)
                {
                    _targetMoveSpeed = Mathf.Lerp(_targetMoveSpeed, _defaultMoveSpeed,
                        _returnTemporarilySpeedUpSmooth * Time.deltaTime);
                    return;
                }

                _disposable.Clear();
            }).AddTo(_disposable);
        }
    }
}