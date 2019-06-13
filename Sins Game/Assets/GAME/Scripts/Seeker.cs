using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public Rigidbody rb;
    public float speedMultiplier;
    public float rotationSpeed;
    [SerializeField] private float maxSpeed;

    // Update is called once per frame
    void Update()
    {
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
        
        rb.AddRelativeTorque(0,Input.GetAxis("Horizontal") * rotationSpeed,0,ForceMode.VelocityChange);
       
    } 
}
