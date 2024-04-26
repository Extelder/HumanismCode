using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerObject : NetworkBehaviour
{
    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerIDNumber;
    [SyncVar] public ulong PlayerSteamID;

    [SyncVar(hook = nameof(PlayerNameUpdate))]
    public string PlayerName;

    private CustomNetworkManager _networkManager;

    private CustomNetworkManager _manager
    {
        get
        {
            if (_networkManager != null)
            {
                return _networkManager;
            }

            return _networkManager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void Awake()
    {
        OnPlayerSpawned.Instance.NewPlayerInScene(this);
    }

    private void OnDisable()
    {
        OnStopClient();
        OnStopLocalPlayer();
    }


    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName());
        gameObject.name = "LocalGamePlayer";
        Lobby.Instance.FindLocalPlayer();
        Lobby.Instance.UpdateLobbyName();
    }

    public override void OnStartClient()
    {
        _manager.Players.Add(this);
        Lobby.Instance.UpdateLobbyName();
        Lobby.Instance.UpdatePlayerList();
    }

    public override void OnStopClient()
    {
        _manager.Players.Remove(this);
        Lobby.Instance.UpdatePlayerList();
    }


    [Mirror.Command]
    private void CmdSetPlayerName(string name)
    {
        this.PlayerNameUpdate(this.PlayerName, name);
    }

    public void PlayerNameUpdate(string OldValue, string NewValue)
    {
        if (isServer)
        {
            this.PlayerName = NewValue;
        }

        if (isClient)
        {
            Lobby.Instance.UpdatePlayerList();
        }
    }
}