using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineRotation : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;

    private CinemachinePOV _cinemachinePOV;

    private void Awake()
    {
        _cinemachinePOV = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    public void DisableRotation()
    {
        _cinemachineInputProvider.enabled = false;
    }

    public void EnableRotation()
    {
        _cinemachineInputProvider.enabled = true;
    }

    public void UpdateSensetivity(Vector2 value)
    {
        _cinemachinePOV.m_VerticalAxis.m_MaxSpeed = value.x;
        _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = value.y;
    }
}