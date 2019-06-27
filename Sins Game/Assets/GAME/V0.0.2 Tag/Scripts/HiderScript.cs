using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Tag
{
    public class HiderScript : NetworkBehaviour
    {
        public NetManager manager;
        // Start is called before the first frame update
        void Start()
        {
            if (!isLocalPlayer) return;
            print("hider spawned");
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!isLocalPlayer) return;
            if (other.gameObject.tag.Equals("Player"))
            {
                manager.SwapOver();
            }
        }
    }
}
