using System.Collections;
using System.Collections.Generic;
using Mirror;
using Tag;
using UnityEngine;

public class NetManager : NetworkManager
{
    private bool first = true;

    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage)
    {
        base.OnServerAddPlayer(conn, extraMessage);
        if (first)
        {
            conn.playerController.gameObject.GetComponent<CharacterBase>().RpcSpawnSeeker();
            first = false;
        }
        else
        {
            conn.playerController.gameObject.GetComponent<CharacterBase>().RpcSpawnHider();
        }
    }
}
