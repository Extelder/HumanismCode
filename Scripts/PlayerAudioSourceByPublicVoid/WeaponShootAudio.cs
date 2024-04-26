using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShootAudio : PlayerAudioSourceByPublicVoid
{
    public void ShootAudio()
    {
        AudioSource?.Play();
    }
}