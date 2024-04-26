using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerSpawned : MonoBehaviour
{
    public static OnPlayerSpawned Instance { get; private set; }

    public event Action<FollowedObject> PlayerSpawned;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Destroy(this);
    }

    public void NewPlayerInScene(PlayerObject playerObject)
    {
        PlayerSpawned?.Invoke(playerObject.GetComponent<FollowedObject>());
    }
}