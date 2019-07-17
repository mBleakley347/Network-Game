using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Mirror;
using Mirror.Websocket;
using Tag;
using UnityEngine;
using UnityEngine.UI;

public class NetManager : NetworkManager
{
    private bool first = true;
    public GameObject seeker;
    public GameObject hider;
    public string address;
    
    private List<NetworkConnection> players = new List<NetworkConnection>();
    private T5 mapScript;

    public override void Awake()
    {
        address = networkAddress;
        base.Awake();
    }

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

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        players.Remove(conn);
        if (conn.playerController.gameObject.GetComponent<SeekerScript>()) SwapOver();
        base.OnServerDisconnect(conn);
    }

    public void SwapOver()
    {
        if (FindObjectOfType<T5>()) mapScript = FindObjectOfType<T5>();
        mapScript.ChangeMap();
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

        for (i = players.Count - 1; i > -1; i--)
        {
            if (players[i].playerController.gameObject.GetComponent<SeekerScript>()) break;
            if (i == 0) SpawnSeeker(players[i]);
        }
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
