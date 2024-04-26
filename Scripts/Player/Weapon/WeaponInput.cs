using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    [SerializeField] private PlayerPhysics _playerPhysics;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerDash _playerDash;
    [SerializeField] private WeaponStateMachine _weaponStateMachine;
    [SerializeField] private bool _rightButtonToShoot = false;

    public PlayerControls Controls { get; private set; }
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Awake()
    {
        Controls = new PlayerControls();
    }

    private void OnEnable()
    {
        Controls.Enable();

        if (!_rightButtonToShoot)
        {
            Controls.Main.Shoot.started += context => CheckShooting();
            Controls.Main.Shoot.canceled += context => _disposable.Clear();
        }

        else
        {
            Controls.Main.GravityHandling.started += context => CheckShooting();
            Controls.Main.GravityHandling.canceled += context => _disposable.Clear();
        }
        
        StartCoroutine(Run());
    }

    private void OnDisable()
    {
        
        Controls.Main.Shoot.started -= context => CheckShooting();
        Controls.Main.Shoot.canceled -= context => _disposable.Clear();
        Controls.Main.GravityHandling.started -= context => CheckShooting();
        Controls.Main.GravityHandling.canceled -= context => _disposable.Clear();
        Controls.Disable();
        _disposable.Clear();
        StopCoroutine(Run());
        StopAllCoroutines();
    }

    private void CheckShooting()
    {
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            if (_rightButtonToShoot && Controls.Main.GravityHandling.IsPressed() && (Time.timeScale >= 1))
            {
                _weaponStateMachine.Shoot();
                return;
            }

            if (Controls.Main.Shoot.IsPressed() && (Time.timeScale >= 1))
            {
                _weaponStateMachine.Shoot();
                return;
            }
        }).AddTo(_disposable);
    }

    private IEnumerator Run()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            if (_weaponStateMachine.StateMachine.CurrentState.CanChangeState == false)
                continue;

            if (_playerMovement.Controls.Main.MoveForwardBackward.ReadValue<float>() == 0 &&
                _playerMovement.Controls.Main.MoveLeftRight.ReadValue<float>() == 0 || _playerDash.Dashing)
            {
            }
            else if (_playerDash.Dashing == false && _playerPhysics.IsGrounded())
            {
                if (Controls.Main.Run.IsPressed())
                {
                    _weaponStateMachine.Run();
                    continue;
                }

                _weaponStateMachine.Walk();
                continue;
            }

            _weaponStateMachine.Idle();
        }
    }
}