using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupJumpad : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerJumpad _jumpad;

    public void Interact()
    {
        _jumpad.OnJumpadPickuped(1);
        Destroy(gameObject);
    }
}