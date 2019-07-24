using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEditorInternal;
using UnityEngine;

namespace Tag
{
    public class HiderScript : NetworkBehaviour
    {
        public NetManager manager;
        private MeshRenderer _renderer;
        private CharacterBase _characterBase;
        [SerializeField] private float invisCD;
        [SerializeField] private float invisCharge;
        [SerializeField] private int invisDurationMult; 
        private float speed;
        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _characterBase = GetComponent<CharacterBase>();
            speed = _characterBase.speedMultiplier;
            if (!isLocalPlayer) return;
            print("hider spawned");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && invisCharge >= invisCD)
            {
                if (isLocalPlayer)
                {
                    CmdInvisible(false);
                    _characterBase.speedMultiplier /= 2;
                }
            }

            if (invisCharge >= invisCD / invisDurationMult)
            {
                if (isLocalPlayer)
                {
                    CmdInvisible(true);
                    _characterBase.speedMultiplier = speed;
                }
            }
            
        }

        /*
        private void OnCollisionEnter(Collision other)
        {
            if (!isLocalPlayer) return;
            if (other.gameObject.tag.Equals("Player"))
            {
                manager.SwapOver();
            }
        }
        */

        [Command]
        public void CmdInvisible(bool value)
        {
            Debug.Log("Invis");
            
           RpcInvisible(value);
        }

        [ClientRpc]
        public void RpcInvisible(bool value)
        {
            _renderer.enabled = false;
        }
    }
}
