using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using UnityEngine;

public class InstantiateCamera : NetworkBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;

    public event Action<Camera> CameraInstantiated;
    public event Action<CinemachineVirtualCamera> CinemachineCameraInstantiated;

    private void Start()
    {
        if (isOwned)
        {
            CameraInstantiated?.Invoke(Instantiate(_camera, transform.position, Quaternion.identity)
                .GetComponent<Camera>());
            CinemachineCameraInstantiated?.Invoke(Instantiate(_cinemachineCamera, transform.position,
                Quaternion.identity));
        }
    }
}