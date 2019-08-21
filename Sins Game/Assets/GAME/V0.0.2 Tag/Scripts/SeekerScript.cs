using System;
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

        
        [SerializeField] private float abilityOneCooldown = 0;
        [SerializeField] private float abilityOneCharge = 0;
        [SerializeField] private float abilityTwoCooldown = 0;
        [SerializeField] private float abilityTwoCharge = 0;

        [SerializeField] private CooldownManager abilityOne;
        [SerializeField] private CooldownManager abilityTwo;


        // Start is called before the first frame update
        void Start()
        {
            if (!isLocalPlayer) return;
            print("Seeker spawned");
            abilityOne = GameObject.Find("AbilityOne").GetComponent<CooldownManager>();
            abilityTwo = GameObject.Find("AbilityTwo").GetComponent<CooldownManager>();

            abilityOneCharge = abilityOneCooldown;
            abilityTwoCharge = abilityTwoCooldown;

        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                
                return;
            }
            if (decoy == null) CmdDecoyLight(true);
            if (Input.GetKeyDown(KeyCode.Space) && abilityOneCharge >= abilityOneCooldown)
            {
                abilityOneCharge = 0;
                CmdDecoyLight(false);
                abilityOne.StartCooldown(abilityOneCooldown);
                
            }
            if (abilityOneCharge < abilityOneCooldown) abilityOneCharge+=Time.deltaTime;
            
            //dhjcbdc
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
