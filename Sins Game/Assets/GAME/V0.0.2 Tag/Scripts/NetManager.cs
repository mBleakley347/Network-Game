using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Mirror;
using Tag;
using UnityEngine;

public class NetManager : NetworkManager
{
    private bool first = true;
    public GameObject seeker;
    public GameObject hider;

    private List<NetworkConnection> players = new List<NetworkConnection>();
    
    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage)
    {
        if (FindObjectOfType<Seeker>()) first = false;
        base.OnServerAddPlayer(conn, extraMessage);
        if (first)
        {
            SpawnSeeker(conn);
            first = false;
        }
        else
        {
            SpawnHider(conn);
        }
        players.Add(conn);
    }

    public void SwapOver()
    {
        int i;
        for (i = players.Count - 1; i > -1; i--)
        {
            if (i != 0)
            {
                if (players[i - 1].playerController.gameObject.GetComponent<SeekerScript>())
                {
                    SpawnSeeker(players[i]);
                    continue;
                }
            }
            SpawnHider(players[i]);
        }
        if (!FindObjectOfType<SeekerScript>()) SpawnSeeker(players[0]);
    }
    
    private void SpawnSeeker(NetworkConnection conn)
    {
        Transform startPos = GetStartPosition();
        GameObject temp = Instantiate(seeker, startPos.position,
            startPos.rotation);
        Destroy(conn.playerController.gameObject);
        NetworkServer.ReplacePlayerForConnection(conn,temp);
        temp.GetComponent<SeekerScript>().manager = this;
    }

    private void SpawnHider(NetworkConnection conn)
    {
        Transform startPos = GetStartPosition();
        GameObject temp = Instantiate(hider, startPos.position,
            startPos.rotation);
        Destroy(conn.playerController.gameObject);
        NetworkServer.ReplacePlayerForConnection(conn, temp);
        temp.GetComponent<HiderScript>().manager = this;
    }
    public override void OnStopHost()
    {
        base.OnStopHost();
        first = true;
    }
}
