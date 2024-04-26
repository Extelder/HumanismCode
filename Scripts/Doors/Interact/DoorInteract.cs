using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Door _door;

    public void Interact()
    {
        _door.TryOpenClose();
    }
}