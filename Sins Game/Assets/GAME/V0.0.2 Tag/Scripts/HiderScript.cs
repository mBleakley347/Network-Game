﻿using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
        [SerializeField] private int sprintCD;
        [SerializeField] private int sprintCharge;
        private float speed;
        [SerializeField] private Camera cam;

        private bool invisActive = false;
        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _characterBase = GetComponent<CharacterBase>();
            speed = _characterBase.speedMultiplier;
            if (!isLocalPlayer) return;
            cam.enabled = true;
            print("hider spawned");
        }

       

        public void FixedUpdate()
        {
            if (!isLocalPlayer) return;
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && sprintCharge >= sprintCD)
            {
                sprintCharge = 0;
                GetComponent<CharacterBase>().speedMultiplier = GetComponent<CharacterBase>().speedMultiplier * 2;
            }
            if (sprintCharge < sprintCD)sprintCharge++;
            //sprintCharge+=Time.deltaTime; use this instead of sprintCharge++
            if (sprintCharge >= (sprintCD / 2))
            {
                GetComponent<CharacterBase>().speedMultiplier = 1;
            }
            if (Input.GetKeyDown(KeyCode.Q) && invisCharge >= invisCD)
            {
                
                    CmdInvisible(false);
                    _characterBase.speedMultiplier /= 2;
                    invisActive = true;
                    invisCharge = 0;
            }

            if (invisCharge >= invisCD / invisDurationMult && invisActive)
            {
                
                    CmdInvisible(true);
                    _characterBase.speedMultiplier = speed;
                    invisActive = false;
            }

            invisCharge++;
            _renderer.enabled = false;
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
            _renderer.enabled = value;
        }
    }
}
