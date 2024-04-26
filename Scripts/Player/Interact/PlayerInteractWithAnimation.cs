using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class PlayerInteractWithAnimation : MonoBehaviour
{
    [SerializeField] private PlayerStop _playerStop;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private CinemachineBrain _cinemachineCamera;

    [Header("Interpolate")] [SerializeField]
    private float _playerInterpolatePositionDuration;

    [SerializeField] private float _cameraInterpolatePositionDuration;

    private Tween _playerInterpolateTween;
    private Tween _cameraInterpolateTween;

    public static PlayerInteractWithAnimation Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Debug.LogError("There are more PlayerInteractWithAnimation");
        Destroy(this);
    }

    private void OnEnable()
    {
        _playerInterpolateTween.Kill();
        _cameraInterpolateTween.Kill();
    }

    public void StopInteraction()
    {
        _cinemachineCamera.enabled = true;
        _playerStop.Play();
    }

    public void Interact(PlayerInteractAnimator interactAnimator, Vector3 needPosition, Vector3 needCameraEuler,
        Action OnAnimationEnd)
    {
        _playerStop.Stop();

        _playerInterpolateTween =
            _player.DOMove(needPosition, _playerInterpolatePositionDuration).OnComplete(() =>
            {
                _cinemachineCamera.enabled = false;
                _cameraInterpolateTween =
                    _camera.DORotate(needCameraEuler, _cameraInterpolatePositionDuration).OnComplete(() =>
                    {
                        CheckIsPlayerAndCameraInNeedTransforms(interactAnimator, OnAnimationEnd);
                    });
            });
    }

    private void CheckIsPlayerAndCameraInNeedTransforms(PlayerInteractAnimator interactAnimator, Action OnAnimationEnd)
    {
        Debug.Log("CheckIsPlayerAndCameraInNeedTransforms");
        _playerInterpolateTween.Kill();
        _cameraInterpolateTween.Kill();
        interactAnimator.InteractAnimation(OnAnimationEnd);
    }
}