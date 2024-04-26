using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStun : MonoBehaviour
{
    public bool Stunned;

    public abstract void Hooked();

    public abstract void Stun();
}