using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class GameObjectChangingByOwner : NetworkBehaviour
{
    [SerializeField] private GameObject _firstGameObject;
    [SerializeField] private GameObject _secondGameObject;

    private void Start()
    {
        if (isOwned)
        {
            _firstGameObject.SetActive(true);
            _secondGameObject.SetActive(false);
        }
        else
        {
            _firstGameObject.SetActive(false);
            _secondGameObject.SetActive(true);
        }
    }
}