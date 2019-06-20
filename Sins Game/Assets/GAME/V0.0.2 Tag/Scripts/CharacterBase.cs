using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Tag
{
    /*
     * Wall Jump Ability
     * 
     */
    public class CharacterBase : NetworkBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private int speedMultiplier;
        [SerializeField] private int rotationSpeed;
        // Start is called before the first frame update
        void Awake()
        {
            if (!isLocalPlayer) return;
            gameObject.AddComponent<Camera>();
            gameObject.AddComponent<AudioListener>();
        }

        void Update()
        {
            if (!isLocalPlayer) return;
            Move();
            Rotate();
        }

        public void Move()
        {
            rb.AddRelativeForce(Vector3.forward * (Input.GetAxis("Vertical") * speedMultiplier),ForceMode.VelocityChange);
            //changed from velocity to incorperate a max speed 
        }

        public void Rotate()
        {
            rb.AddRelativeTorque(0, Input.GetAxis("Horizontal") * rotationSpeed, 0, ForceMode.VelocityChange);
        } 
        
        [ClientRpc]
        public void RpcSpawnHider()
        {
            gameObject.AddComponent<HiderScript>();
            
        }
        public void RpcSpawnSeeker()
        {
            gameObject.AddComponent<SeekerScript>();
            
        }
    }
}
