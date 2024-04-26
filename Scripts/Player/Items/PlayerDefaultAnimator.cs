using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDefaultAnimator : DefaultAnimator
{
    public abstract void Walk();

    public abstract void Run();

    public abstract void Use();

    public abstract void Jump();

    public abstract void NotInAir();
    
    public abstract void InAir();
}