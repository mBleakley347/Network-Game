using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tag
{
    /*
     * Collision to end game
     * 
     */
    public class SeekerScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Equals("Hider"))
            {
                
            }
        }
    }
}
