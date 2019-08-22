using System;
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
        [SerializeField] private float abilityOneCooldown = 0f;
        [SerializeField] private float abilityOneCharge = 0f;
        [SerializeField] private int invisDurationMult = 0;
        [SerializeField] private float abilityTwoCooldown = 0;
        [SerializeField] private float abilityTwoCharge = 0;
        [SerializeField] private CooldownManager abilityOne;
        [SerializeField] private CooldownManager abilityTwo;

        private float speed = 0;
        [SerializeField] private Camera cam = null;

        [SerializeField] private PostProcessVolume postProcesser;

        private bool invisActive = false;
        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _characterBase = GetComponent<CharacterBase>();
            speed = _characterBase.speedMultiplier;
            if (!isLocalPlayer) return;
            cam.enabled = true;
            postProcesser.enabled = false;
            print("hider spawned");
            abilityOne = GameObject.Find("AbilityOne").GetComponent<CooldownManager>();
            abilityTwo = GameObject.Find("AbilityTwo").GetComponent<CooldownManager>();

            abilityOneCharge = abilityOneCooldown;
            abilityTwoCharge = abilityTwoCooldown;
        }

       

        public void FixedUpdate()
        {
            if (!isLocalPlayer) return;
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && abilityTwoCharge >= abilityTwoCooldown)
            {
                abilityTwoCharge = 0;
                //make another ability here where commented out code is below
                GetComponent<CharacterBase>().speedMultiplier = GetComponent<CharacterBase>().speedMultiplier * 2;
                abilityTwo.StartCooldown(abilityTwoCooldown);
                
                
            }
            if (abilityTwoCharge >= (abilityTwoCooldown / 2))
            {
                GetComponent<CharacterBase>().speedMultiplier = 1;
            }
            if (abilityTwoCharge < abilityTwoCooldown) abilityTwoCharge+=Time.deltaTime;
        

            if (Input.GetKeyDown(KeyCode.Space) && abilityOneCharge >= abilityOneCooldown)
            {
                CmdInvisible(false);
                _characterBase.speedMultiplier /= 2;
                invisActive = true;
                abilityOneCharge = 0;
                postProcesser.enabled = true;
                abilityOne.StartCooldown(abilityOneCooldown);
            }

            if (abilityOneCharge >= abilityOneCooldown / invisDurationMult && invisActive)
            {
                CmdInvisible(true);
                _characterBase.speedMultiplier = speed;
                invisActive = false;
                postProcesser.enabled = false;
            }

            abilityOneCharge += Time.deltaTime;
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
