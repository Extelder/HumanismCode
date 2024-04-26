using System;
using Mirror;
using UnityEngine;


public class DestroyMonoBehavioursWithAuthority : MonoBehaviour
{
    [SerializeField] private NetworkIdentity _identity;
    [SerializeField] private MonoBehaviour[] _behaviours;

    private void Start()
    {
        if (_identity.isOwned == false)
        {
            for (int i = 0; i < _behaviours.Length; i++)
            {
                Destroy(_behaviours[i]);
            }
        }
    }
}