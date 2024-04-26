using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundByAnimationEvent : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void AnimationEventPlaySound()
    {
        _audioSource.Play();
    }
}