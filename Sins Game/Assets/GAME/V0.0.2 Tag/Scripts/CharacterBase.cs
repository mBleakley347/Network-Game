using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering;

namespace Tag
{
    /*
     * Wall Jump Ability
     * 
     */
    public class CharacterBase : NetworkBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] public float speedMultiplier;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private GameObject light;
        
        // Start is called before the first frame update
        void Start()
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
    }
}
