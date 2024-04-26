using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeaponItem : PickupItem
{
    public override void Interact()
    {
        ItemPickuped?.Invoke(CurrentItem);
        RadialMenu.Instance.OnWeaponPickuped(CurrentItem);
        Destroy(gameObject);
    }
}