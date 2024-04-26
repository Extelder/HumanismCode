using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class WeaponAnimationState : State
{
    public WeaponAnimator WeaponAnimator;
    public abstract override void Enter();
}