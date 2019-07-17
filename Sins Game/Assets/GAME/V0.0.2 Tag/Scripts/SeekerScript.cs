using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Websocket;
using UnityEngine;

namespace Tag
{
    /*
     * Collision to end game
     * 
     */
    public class SeekerScript : NetworkBehaviour
    {
        public NetManager manager;

        public GameObject light;

        private int decoyCD;
        private int decoyCharge;
        // Start is called before the first frame update
        void Start()
        {
            if (!isLocalPlayer) return;
            print("Seeker spawned");
        }

        private void Update()
        {
            if (Input.GetButtonDown("1") && decoyCharge >= decoyCD)
            {
                
            }
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

        public void DecoyLight()
        {
            light.transform.Translate(Vector3.forward);
        }
    }
}
