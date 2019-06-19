using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Tag
{
    public class HiderScript : NetworkBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (!isLocalPlayer) return;
            print("hider spawned");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
