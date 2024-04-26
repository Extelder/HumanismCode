using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerHook : RaycastBehaviour
{
    [SerializeField] private CinemachineRotation _cinemachineRotation;
    [SerializeField] private PlayerHookVisualize _hookVisualize;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerGoreKill _goreKill;
    [SerializeField] private GameObject _overlayCamera;
    [SerializeField] private GameObject _hookInteractableDetectedIndicator;
    [SerializeField] private float _hookInteractableDetectedRate = 0.1f;
    [SerializeField] private float _enemyHookedLerp;
    [SerializeField] private float _enemyHookedDistanceToKill;
    [SerializeField] private int _overlayLayerId;
    [SerializeField] private PlayerPhysics _physics;

    private PlayerControls _controls;
    private CharacterController _characterController;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private bool _hoocked;

    private void Awake()
    {
        _controls = new PlayerControls();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Main.ThrowHook.performed += context => CheckEnemyStun();
        StartCoroutine(CheckingEnemyStunDetect());
    }

    private void OnDisable()
    {
        _controls.Main.ThrowHook.performed -= context => CheckEnemyStun();

        _controls.Disable();
        _disposable.Clear();
        StopAllCoroutines();
    }

    public void Kill(NavMeshEnemyStun navMeshEnemyStun)
    {
        navMeshEnemyStun.gameObject.SetActive(false);
    }

    private IEnumerator CheckingEnemyStunDetect()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_hookInteractableDetectedRate);
            if (GetHitCollider(out Collider collider) && _hoocked == false)
            {
                if (collider.TryGetComponent<NavMeshEnemyStun>(out NavMeshEnemyStun enemyStun))
                {
                    if (enemyStun.Stunned)
                    {
                        _hookInteractableDetectedIndicator.SetActive(true);
                        continue;
                    }
                }
            }

            _hookInteractableDetectedIndicator.SetActive(false);
        }
    }

    public void CheckEnemyStun()
    {
        if (GetHitCollider(out Collider collider) && _hoocked == false)
        {
            if (collider.TryGetComponent<NavMeshEnemyStun>(out NavMeshEnemyStun enemyStun))
            {
                EnemyHealth enemyHealth = enemyStun.GetComponent<EnemyHealth>();
                if (enemyHealth.Dead)
                    return;

                if (enemyStun.Stunned)
                {
                    _cinemachineRotation.DisableRotation();
                    _playerHealth.DamageBlocked = true;
                    _characterController.enabled = false;
                    _hoocked = true;

                    _overlayCamera.SetActive(false);
                    enemyStun.Hooked();

                    _hookVisualize.ThrowHook(enemyStun.transform.position,
                        () =>
                        {
                            Observable.EveryFixedUpdate().Subscribe(_ =>
                            {
                                enemyStun.transform.position =
                                    Vector3.Lerp(enemyStun.transform.position, transform.position,
                                        _enemyHookedLerp * Time.deltaTime);

                                if (Vector3.SqrMagnitude(enemyStun.transform.position - transform.position) <=
                                    _enemyHookedDistanceToKill)
                                {
                                    _hookVisualize.BreackHook();

                                    _disposable.Clear();

                                    _goreKill.Kill(enemyHealth, () =>
                                    {
                                        _playerHealth.DamageBlocked = false;
                                        _overlayCamera.SetActive(true);
                                        _characterController.enabled = true;
                                        _hoocked = false;
                                    });
                                }
                            }).AddTo(_disposable);

                            _hookVisualize.BacktrackHook(enemyStun.transform.position);
                        });
                }
            }
        }
    }
}