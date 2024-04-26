using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private string _openBoolName;

    public void SetOpenBoolTrue()
    {
        _animator.SetBool(_openBoolName, true);
        _audioSource.Play();
    }

    public void SetOpenBoolFalse()
    {
        _animator.SetBool(_openBoolName, false);
        _audioSource.Play();
    }
}