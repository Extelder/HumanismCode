using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOverheatAudio : PlayerAudioSourceByPublicVoid
{
    public void OverheatAudio()
    {
        AudioSource?.Play();
    }
}