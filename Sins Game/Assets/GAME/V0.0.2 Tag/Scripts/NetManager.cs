using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Mirror;
using Tag;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetManager : NetworkManager
{
    private bool first = true;
    public GameObject seeker;
    public GameObject hider;

    private NetworkConnection currentSeeker;
    private NetworkConnection currentHider;

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
    }

    public void SwapOver()
    {
        ///Todo Test this
        NetworkConnection tempHider = currentHider;
        NetworkConnection tempSeeker = currentSeeker;
        SpawnSeeker(tempHider);
        SpawnHider(tempSeeker);
    }

    private void SpawnSeeker(NetworkConnection conn)
    {
        Transform startPos = GetStartPosition();
        GameObject temp = Instantiate(seeker, startPos.position,
            startPos.rotation);
        Destroy(conn.playerController.gameObject);
        currentSeeker = conn;
        NetworkServer.ReplacePlayerForConnection(conn,temp);
        temp.GetComponent<SeekerScript>().manager = this;
    }

    private void SpawnHider(NetworkConnection conn)
    {
        Transform startPos = GetStartPosition();
        GameObject temp = Instantiate(hider, startPos.position,
            startPos.rotation);
        Destroy(conn.playerController.gameObject);
        currentHider = conn;
        NetworkServer.ReplacePlayerForConnection(conn, temp);
        temp.GetComponent<HiderScript>().manager = this;
    }
    public override void OnStopHost()
    {
        base.OnStopHost();
        first = true;
    }
}
