using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerWallRunTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineJobing _jobing;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private MoveRotationByZ _moveRotationByZ;
    [SerializeField] private PlayerJump _playerJump;
    [SerializeField] private PlayerPhysics _playerPhysics;
    [SerializeField] private float _cameraAngle;
    [SerializeField] private float _cameraLerpAngle;

    private CompositeDisposable _disposable = new CompositeDisposable();
    private CompositeDisposable _groundCheckDisposable = new CompositeDisposable();

    private bool _isRunning;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<RunWall>(out RunWall runWall) && _playerPhysics.IsGrounded() == false && !_isRunning)
        {
            _isRunning = true;
            BeginRunning();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<RunWall>(out RunWall runWall) && _isRunning == true)
        {
            StopRunning();
        }
    }

    private void OnDisable()
    {
        _disposable.Clear();
        _groundCheckDisposable.Clear();
    }

    private void BeginRunning()
    {
        _moveRotationByZ.enabled = false;
        _playerPhysics.Velocity.y = 0;
        _jobing.enabled = false;
        _movement.ChangeSpeedToWallRunning();
        _playerJump.GravityMultiplayer = 0.15f;
        _playerJump.ResetJumps();


        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_cinemachineVirtualCamera.m_Lens.Dutch != _cameraAngle)
            {
                _cinemachineVirtualCamera.m_Lens.Dutch = Mathf.Lerp(_cinemachineVirtualCamera.m_Lens.Dutch,
                    _cameraAngle, _cameraLerpAngle * Time.deltaTime);
            }
            else
            {
                _disposable.Clear();
            }
        }).AddTo(_disposable);

        Observable.EveryLateUpdate().Subscribe(_ =>
        {
            if (_playerPhysics.IsGrounded()) StopRunning();
        }).AddTo(_groundCheckDisposable);
    }

    private void StopRunning()
    {
        _groundCheckDisposable.Clear();
        _disposable.Clear();
        _playerJump.GravityMultiplayer = 1f;
        _jobing.DisableAllAnimations();
        _jobing.enabled = true;
        _movement.ChangeSpeedToDefault();
        _isRunning = false;
        _moveRotationByZ.enabled = true;
    }
}