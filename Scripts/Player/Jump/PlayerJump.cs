using System;
using UniRx;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _jumpDelay;
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private float _firstJumpHeight;
    [SerializeField] private float _secondJumpHeight;
    [SerializeField] private float _gravity = 3f;
    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private int _maxJumps;
    [SerializeField] private int _currentJumps;

    public float GravityMultiplayer = 1f;

    private PlayerControls _controls;
    private bool _canJump = true;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Awake()
    {
        _controls = new PlayerControls();
        _currentJumps = _maxJumps;
    }

    private void OnEnable()
    {
        _controls.Enable();

        _controls.Main.Jump.performed += context => Jump();
    }

    private void OnDisable()
    {
        _disposable.Clear();

        _controls.Disable();
    }

    private void Update()
    {
        if (((_characterController.collisionFlags & CollisionFlags.Above) != 0))
        {
            _physics.Velocity.y = -3f;
            _physics.Velocity.y += _gravity * Time.deltaTime * GravityMultiplayer;
        }

        if (_physics.IsGrounded() && _physics.Velocity.y < 0.0f)
        {
            _physics.Velocity.y = -0.05f;
            ResetJumps();
        }
        else
        {
            if (_physics.Velocity.y > _gravity)
                _physics.Velocity.y += _gravity * Time.deltaTime * GravityMultiplayer;
            else
                _physics.Velocity.y = _gravity * GravityMultiplayer;
        }
    }

    private void Jump()
    {
        if (_currentJumps <= 0)
            return;

        if (!_canJump)
            return;

        _jumpSound?.Play();
        _currentJumps--;
        _physics.Velocity.y = 0;
        if (_currentJumps == 1)
            _physics.Velocity.y += _firstJumpHeight;
        else
            _physics.Velocity.y += _secondJumpHeight;

        _canJump = false;
        Observable.Timer(TimeSpan.FromSeconds(_jumpDelay)).Subscribe(_ => { _canJump = true; }).AddTo(_disposable);
    }

    public void JumpWithOther(float value)
    {
        _physics.Velocity.y = 0;
        _physics.Velocity.y += value;
    }

    public void ResetJumps()
    {
        _currentJumps = _maxJumps;
    }
}