using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _interactAnimationTrigger;

    private Action _currentAnimationEnd;

    public void InteractAnimation(Action OnAnimationEnd)
    {
        _animator.SetTrigger(_interactAnimationTrigger);
        _currentAnimationEnd = OnAnimationEnd;
    }

    public void InteractAnimationEnd()
    {
        _currentAnimationEnd?.Invoke();
    }
}