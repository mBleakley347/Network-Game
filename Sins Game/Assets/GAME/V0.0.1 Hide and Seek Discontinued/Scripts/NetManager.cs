using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetManager : NetworkManager
{

    public Transform SpawnLocation;
    public GameObject[] Sins;
    public GameObject Hider;
/*
    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage)
    {
        Transform start = SpawnLocation;
        GameObject player = Instantiate(Hider, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }
*/
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
    }
}
