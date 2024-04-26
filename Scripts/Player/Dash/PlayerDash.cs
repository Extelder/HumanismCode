using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Image _dashImage;
    [SerializeField] private Image _dashReturnImage;
    [SerializeField] private Image _dashReturnImage2;
    [SerializeField] private AnimationCurve _dashReturnAnimationCurve;
    [SerializeField] private AnimationCurve _dashReturnAnimationCurve2;
    [SerializeField] private float _dashImageEffectTime = 0.1f;
    [Space(10)] [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerPhysics _physics;
    [Space(10)] [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private AudioSource _dashSound;
    [SerializeField] private float _dashesReturnTime;
    [Space(10)] [SerializeField] private int _maxDashes;

    private int _currentDashes;
    private PlayerControls _controls;
    private bool _canDash = true;
    private CompositeDisposable _disposable = new CompositeDisposable();
    private CompositeDisposable _dashImageDisposable = new CompositeDisposable();
    private CompositeDisposable _dashImageReturnDispoable = new CompositeDisposable();
    private CompositeDisposable _dashImageReturnDispoable2 = new CompositeDisposable();

    public bool Dashing { get; private set; }

    private void OnEnable()
    {
        _currentDashes = _maxDashes;
        _controls = new PlayerControls();

        _controls.Enable();
        _controls.Main.Dash.performed += context => StartCoroutine(DashCoroutine());
    }

    private void OnDisable()
    {
        _controls.Main.Dash.performed -= context => StartCoroutine(DashCoroutine());
        _controls.Disable();

        _disposable.Clear();
        _dashImageDisposable.Clear();
        _dashImageReturnDispoable?.Clear();
        _dashImageReturnDispoable2?.Clear();
    }

    private IEnumerator DashCoroutine()
    {
        if (_currentDashes <= 0)
            yield break;

        if (!_canDash)
            yield break;

        _dashSound?.Play();

        Dashing = true;

        _canDash = false;

        float timerValue;

        float startTime = Time.time;


        if (_dashReturnImage.fillAmount == 1)
        {
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
        }
        else
        {
            float currentTime2 = 0;
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    _dashReturnImage2.fillAmount = _dashReturnAnimationCurve2.Evaluate(currentTime2);
                    currentTime2 += Time.deltaTime;
                    if (_dashReturnImage2.fillAmount >= 1f)
                        _dashImageReturnDispoable2?.Clear();
                })
                .AddTo((_dashImageReturnDispoable2));
        }

        _currentDashes--;

        Vector3 inputDirection = new Vector3(_controls.Main.MoveLeftRight.ReadValue<float>(), 0,
            _controls.Main.MoveForwardBackward.ReadValue<float>());

        if (inputDirection.sqrMagnitude <= 0)
        {
            inputDirection.z = 1;
        }

        Vector3 direction = transform.TransformDirection(inputDirection);

        Observable.Timer(TimeSpan.FromSeconds(_dashDelay)).Subscribe(_ =>
        {
            _dashImageDisposable.Clear();

            LerpDashImageAlpha(0);
            _canDash = true;
        }).AddTo(_disposable);

        Observable.Timer(TimeSpan.FromSeconds(_dashesReturnTime)).Subscribe(_ =>
        {
            if (_currentDashes + 1 <= _maxDashes) _currentDashes++;
        }).AddTo(_disposable);


        _dashImageDisposable.Clear();

        LerpDashImageAlpha(0.6f);

        while (Time.time < startTime + _dashTime)
        {
            _physics.Velocity = new Vector3(0, 0, 0);
            _physics.enabled = false;

            _characterController.Move(direction * _dashSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        Dashing = false;
        _physics.enabled = true;
    }

    private void LerpDashImageAlpha(float needAlpha)
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            float a = Mathf.Lerp(_dashImage.color.a, needAlpha, _dashImageEffectTime * Time.deltaTime);
            _dashImage.color = new Color(_dashImage.color.r, _dashImage.color.g, _dashImage.color.b, a);
        }).AddTo(_dashImageDisposable);
    }
}