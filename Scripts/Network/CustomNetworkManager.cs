using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField] private PlayerObject _playerObject;

    public List<PlayerObject> Players { get; } = new List<PlayerObject>();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            Instantiate(playerPrefab, GameObject.Find("SpawnPoint").transform.position, Quaternion.identity);
            PlayerObject playerInstance = Instantiate(_playerObject, GameObject.Find("SpawnPoint").transform.position,
                Quaternion.identity);

            playerInstance.ConnectionID = conn.connectionId;
            playerInstance.PlayerIDNumber = Players.Count + 1;
            playerInstance.PlayerSteamID =
                (ulong) SteamMatchmaking.GetLobbyMemberByIndex((CSteamID) SteamLobby.Instance.CurrentLobbyID,
                    Players.Count);

            NetworkServer.AddPlayerForConnection(conn, playerInstance.gameObject);
        }
    }
}