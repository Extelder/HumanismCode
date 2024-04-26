using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorAnimator _animator;
    [SerializeField] private bool _locked;
    [SerializeField] private bool _opened;

    public event Action Unlocked;
    public event Action Locked;

    public void Open()
    {
        if (_locked)
            return;
        _animator.SetOpenBoolTrue();
        _opened = true;
    }

    public void Close()
    {
        if (_locked)
            return;
        _animator.SetOpenBoolFalse();
        _opened = false;
    }

    public void TryOpenClose()
    {
        if (_opened)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void UnLock()
    {
        Debug.Log("Unlocked");
        Unlocked?.Invoke();
        _locked = false;
    }


    public void Lock(bool closeDoor = false)
    {
        if (closeDoor)
        {
            Close();
        }

        _locked = true;
        Locked?.Invoke();
    }
}