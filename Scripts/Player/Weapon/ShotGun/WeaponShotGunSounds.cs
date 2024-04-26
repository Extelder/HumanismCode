using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotGunSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _shootSource;
    [SerializeField] private AudioSource _firstChikSource;
    [SerializeField] private AudioSource _secondChikSource;

    public void PlayShootSound()
    {
        _shootSource.Play();
    }

    public void PlayFirstChikSound()
    {
        _firstChikSource.Play();
    }

    public void PlaySecondChikSound()
    {
        _secondChikSource.Play();
    }
}