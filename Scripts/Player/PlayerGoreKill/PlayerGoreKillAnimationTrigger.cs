using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoreKillAnimationTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioOnGoreKill;
    [SerializeField] private PlayerGoreKill _playerGoreKill;
    [SerializeField] private GameObject _trail;

    public void GoreKill()
    {
        _playerGoreKill.KillAnimationTrigger();
    }

    public void SetTrailActive(int value)
    {
        _trail.SetActive(Convert.ToBoolean(value));
        _audioOnGoreKill.Play();
    }
}