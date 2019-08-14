﻿using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Websocket;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tag
{
    /*
     * Collision to end game
     * 
     */
    public class SeekerScript : NetworkBehaviour
    {
        public NetManager manager;

        public Light personalLight;
        public GameObject decoyLight;
        public GameObject decoy;

        [SerializeField] private int decoyCD = 0;
        [SerializeField] private int decoyCharge = 0;

        // Start is called before the first frame update
        void Start()
        {
            if (!isLocalPlayer) return;
            print("Seeker spawned");
        }

        private void FixedUpdate()
        {
            if (!localPlayerAuthority)
            {
                
                return;
            }
            if (decoy == null) CmdDecoyLight(true);
            if (Input.GetKeyDown(KeyCode.E) && decoyCharge >= decoyCD)
            {
                decoyCharge = 0;
                CmdDecoyLight(false);
            }
            if (decoyCharge != decoyCD)decoyCharge++;
        }


        private void OnCollisionEnter(Collision other)
        {
            if (!isLocalPlayer) return;
            if (other.gameObject.tag.Equals("Player"))
            {
                CmdSwap();
            }
        }

        [Command]
        public void CmdSwap()
        {
            manager.SwapOver();
        }
        [Command]
        public void CmdDecoyLight(bool value)
        {
            if (!value)
            {
                decoy = Instantiate(decoyLight, transform.position, transform.rotation);
                NetworkServer.Spawn(decoy);
            }

            personalLight.enabled = value;
            RpcDecoy(value);
        }

        [ClientRpc]
        public void RpcDecoy(bool value)
        {
            personalLight.enabled = value;
        }
    }
}
