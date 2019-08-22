using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Tag
{
    public class HiderScript : NetworkBehaviour
    {
        public NetManager manager;
        public MeshRenderer _renderer;
        public MeshRenderer _renderer2;
        private CharacterBase _characterBase;
        [SerializeField] private float abilityOneCooldown = 0f;
        [SerializeField] private float abilityOneCharge = 0f;
        [SerializeField] private float invisDurationMult = 0;
        [SerializeField] private float abilityTwoCooldown = 0;
        [SerializeField] private float abilityTwoCharge = 0;
        [SerializeField] private CooldownManager abilityOne;
        [SerializeField] private CooldownManager abilityTwo;
        private GameObject icon;
        private GameObject icon2;

        private float speed = 0;
        [SerializeField] private Camera cam = null;

        [SerializeField] private PostProcessVolume postProcesser;

        private bool invisActive = false;
        // Start is called before the first frame update
        void Start()
        {
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
            icon = GameObject.Find("footicon 2");
            icon2 = GameObject.Find("Fire");
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
                icon.GetComponent<Image>().color = Color.green;


            }
            if (abilityTwoCharge >= (abilityTwoCooldown / 2))
            {
                icon.GetComponent<Image>().color = Color.white;

                GetComponent<CharacterBase>().speedMultiplier = 1;
            }
            if (abilityTwoCharge < abilityTwoCooldown) abilityTwoCharge+=Time.deltaTime;
        

            if (Input.GetKeyDown(KeyCode.Space) && abilityOneCharge >= abilityOneCooldown)
            {
                icon2.GetComponent<Image>().color = Color.green;

                CmdInvisible(false);
                //_characterBase.speedMultiplier /= 2;
                invisActive = true;
                abilityOneCharge = 0;
                postProcesser.enabled = true;
                abilityOne.StartCooldown(abilityOneCooldown);
            }

            if (abilityOneCharge >= abilityOneCooldown / invisDurationMult && invisActive)
            {
                icon2.GetComponent<Image>().color = Color.white;

                CmdInvisible(true);
                //_characterBase.speedMultiplier = speed;
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
            _renderer2.enabled = value;
        }
    }
}
