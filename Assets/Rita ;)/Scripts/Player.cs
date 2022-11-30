using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using Mirror;
using TMPro;


public class Player : NetworkBehaviour
{
    public TextMeshProUGUI tmpro;
    public string playerName;
    public int playerID;
    public override void OnStartClient()
    {
        playerID = NetworkServer.connections.Keys.Count-1;
        playerName = "Player " + playerID;
        tmpro.text = playerName;

    }
    public void OnDisconnect()
    {

    }
}