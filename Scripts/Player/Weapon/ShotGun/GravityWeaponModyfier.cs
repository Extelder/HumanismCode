using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GravityWeaponModyfier : RaycastBehaviour
{
    [SerializeField] private float _pushForce;
    [SerializeField] private Transform _gravityGunHandlPoint;
    [SerializeField] private int _garvityGunHandlObjectLerpSpeed;

    private PlayerControls _playerControls;
    private GravityGunInteractable _currentGravityGunInteractable;
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Main.GravityHandling.started += context => StartHandling();
        _playerControls.Main.GravityHandling.canceled += context => StopHandling();
    }

    private void OnDisable()
    {
        _playerControls.Main.GravityHandling.started -= context => StartHandling();
        _playerControls.Main.GravityHandling.canceled -= context => StopHandling();
        _playerControls.Main.GravityGunImpulse.performed -= context => PushObject();
        _playerControls.Disable();
        _disposable.Clear();
    }

    private void StartHandling()
    {
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            if (_currentGravityGunInteractable != null)
            {
                _currentGravityGunInteractable.UpdateHandling();
                Vector3 directionToPoint =
                    _gravityGunHandlPoint.position - _currentGravityGunInteractable.transform.position;
                float distanceToPoint = directionToPoint.magnitude;

                _currentGravityGunInteractable.Rigidbody.velocity =
                    directionToPoint * _garvityGunHandlObjectLerpSpeed * distanceToPoint * Time.deltaTime;

                return;
            }

            if (GetHitCollider(out Collider collider))
            {
                if (collider.TryGetComponent<GravityGunInteractable>(
                    out GravityGunInteractable gravityGunInteractable) && gravityGunInteractable.CanHandl)
                {
                    _currentGravityGunInteractable = gravityGunInteractable;
                    _playerControls.Main.GravityGunImpulse.performed += context => PushObject();
                    _currentGravityGunInteractable.StartHandling();
                }
            }
        }).AddTo(_disposable);
    }

    private void StopHandling()
    {
        _disposable.Clear();
        if (_currentGravityGunInteractable != null)
            _currentGravityGunInteractable.StopHandling();
        _currentGravityGunInteractable = null;
        _playerControls.Main.GravityGunImpulse.performed -= context => PushObject();
    }

    private void PushObject()
    {
        _currentGravityGunInteractable.Rigidbody.AddForce(Camera.forward * _pushForce, ForceMode.Impulse);
        _currentGravityGunInteractable.Pushed = true;
        _currentGravityGunInteractable.Impulsed();
        StopHandling();
    }
}