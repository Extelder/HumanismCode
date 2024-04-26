using System;
using Cinemachine;
using UnityEngine;

public class MoveRotationByZ : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private float _timeToDefaultValue;
    [SerializeField] private float _angle;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private PlayerControls _controls;

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        if (_controls.Main.MoveLeftRight.ReadValue<float>() > 0)
        {
            _cinemachineVirtualCamera.m_Lens.Dutch =
                Mathf.Lerp(_cinemachineVirtualCamera.m_Lens.Dutch, -_angle, _time * Time.deltaTime);
            return;
        }

        if (_controls.Main.MoveLeftRight.ReadValue<float>() < 0)
        {
            _cinemachineVirtualCamera.m_Lens.Dutch =
                Mathf.Lerp(_cinemachineVirtualCamera.m_Lens.Dutch, _angle, _time * Time.deltaTime);
            return;
        }

        if (Mathf.Abs(_cinemachineVirtualCamera.m_Lens.Dutch) < 0.1f)
        {
            _cinemachineVirtualCamera.m_Lens.Dutch = 0;
            return;
        }

        _cinemachineVirtualCamera.m_Lens.Dutch =
            Mathf.Lerp(_cinemachineVirtualCamera.m_Lens.Dutch, 0, _timeToDefaultValue * Time.deltaTime);
    }
}