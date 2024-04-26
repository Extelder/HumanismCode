using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private PlayerMovement _movement;

    private PlayerControls _controls;

    private bool _running;

    private void OnEnable()
    {
        _controls = new PlayerControls();
        _controls.Enable();

        _controls.Main.Run.started += context => StartRunning();
        _controls.Main.Run.canceled += context => StopRunning();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void StartRunning()
    {
        _running = true;
        _movement.ChangeSpeed(_runSpeed);
    }

    private void StopRunning()
    {
        _running = false;
        _movement.ChangeSpeedToDefault();
    }

    public bool Running() => _running;
}