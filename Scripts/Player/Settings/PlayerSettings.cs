using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private PlayerStop _playerStop;
    [SerializeField] private PlayerCursor _playerCursor;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private int _menuSceneID;

    public ReactiveProperty<bool> Opened = new ReactiveProperty<bool>();

    private PlayerControls _controls;

    private bool _stoppedInOpenMenu = false;
    private bool _enabledCursorInOpenMenu = false;

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Main.Settings.performed += context => OpenClose();
    }

    private void OnDisable()
    {
        _controls.Main.Settings.performed -= context => OpenClose();
        _controls.Disable();
    }

    public void Open()
    {
        _enabledCursorInOpenMenu = _playerCursor.Enabled;
        _playerCursor.Enable();
        _canvas.enabled = true;
        _stoppedInOpenMenu = _playerStop.Stopped;
        _playerStop.Stop();
        Opened.Value = true;
    }

    public void Resume()
    {
        Close();
    }

    public void Quit()
    {
        SceneManager.LoadScene(_menuSceneID);
    }

    private void OpenClose()
    {
        if (Opened.Value)
        {
            Close();
        }
        else
        {
            Open();
        }
    }                   

    public void Close()
    {
        _canvas.enabled = false;
        if (!_stoppedInOpenMenu)
            _playerStop.Play();
        if (!_enabledCursorInOpenMenu)
            _playerCursor.Disable();

        Opened.Value = false;
    }
}