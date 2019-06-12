using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CharacterControlBase : NetworkBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        Hider temp = gameObject.AddComponent<Hider>();
        temp.speedMultiplier = 5;
        temp.rotationSpeed = 5;
        temp.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
