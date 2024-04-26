using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerStop : MonoBehaviour
{
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GameObject _overlatCamera;

    public bool Stopped { get; private set; }

    public void Play()
    {
        Stopped = false;
        _overlatCamera.SetActive(true);
        _characterController.enabled = true;
        _cinemachineInputProvider.enabled = true;
    }

    public void Stop()
    {
        Stopped = true;
        _overlatCamera.SetActive(false);
        _cinemachineInputProvider.enabled = false;
        _characterController.enabled = false;
    }
}