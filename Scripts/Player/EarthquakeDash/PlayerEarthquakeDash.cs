using System;
using System.Collections;
using System.Collections.Generic;
using RayFire;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEarthquakeDash : MonoBehaviour
{
    [Space(10)] [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private Pool _earthquakeCrackPool;
    [SerializeField] private Image _dashReturnImage;
    [SerializeField] private AnimationCurve _dashReturnAnimationCurve;

    [SerializeField] private LayerMask _earthquakeCrackLayerMask;
    [SerializeField] private Transform _earthquakeCrackCheckTransform;
    [SerializeField] private float _earthquakeCrackCheckRadius;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashReturnTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private int _maxDashes;

    private PlayerControls _controls;
    private CompositeDisposable _disposable = new CompositeDisposable();
    private CompositeDisposable _dashImageReturnDispoable = new CompositeDisposable();

    private Collider[] _colliders = new Collider[7];
    private int _dashes;

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _dashes = _maxDashes;
        _controls.Enable();
        _controls.Main.EarthquakeDash.performed += context => Dash();
    }

    private void OnDisable()
    {
        _controls.Main.EarthquakeDash.performed -= context => Dash();
        _controls.Disable();
        _disposable.Clear();
        _dashImageReturnDispoable?.Clear();
    }

    private void Dash()
    {
        if (_dashes > 0 && !_characterController.isGrounded)
            StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        _dashes--;

        Observable.Timer(TimeSpan.FromSeconds(_dashReturnTime)).Subscribe(_ => { _dashes++; }).AddTo(_disposable);

        float startTime = Time.time;

        float currentTime = 0;
        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                _dashReturnImage.fillAmount = _dashReturnAnimationCurve.Evaluate(currentTime);
                currentTime += Time.deltaTime;
                if (_dashReturnImage.fillAmount >= 1f)
                    _dashImageReturnDispoable?.Clear();
            })
            .AddTo((_dashImageReturnDispoable));

        Vector3 inputDirection = -Vector3.up;

        Vector3 direction = inputDirection;

        _colliders = new Collider[10];

        while (Time.time < startTime + _dashTime)
        {
            _physics.Velocity = new Vector3(0, 0, 0);
            _physics.enabled = false;

            _characterController.Move(direction * _dashSpeed * Time.deltaTime);

            Physics.OverlapSphereNonAlloc(_earthquakeCrackCheckTransform.position, _earthquakeCrackCheckRadius,
                _colliders, _earthquakeCrackLayerMask);

            foreach (var other in _colliders)
            {
                if (other == null)
                    continue;
                if (other.TryGetComponent<RayfireRigid>(out RayfireRigid rayfireRigid))
                {
                    rayfireRigid.ApplyDamage(150, transform.position, 15);
                    _physics.enabled = true;
                    yield break;
                }

                if (other.gameObject != this.gameObject)
                {
                    _earthquakeCrackPool.GetFreeElement(_earthquakeCrackCheckTransform.position,
                        Quaternion.identity);
                    _physics.enabled = true;
                    yield break;
                }
            }

            yield return new WaitForFixedUpdate();
        }

        _physics.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_earthquakeCrackCheckTransform.position, _earthquakeCrackCheckRadius);
    }
}