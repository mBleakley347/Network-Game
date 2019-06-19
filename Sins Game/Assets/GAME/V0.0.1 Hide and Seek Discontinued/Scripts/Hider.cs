using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Hider : MonoBehaviour
{
    /*needs to include:
     * 
     * Movement
     * Use Ability
     * ability will be in seperate script
     */

    public Rigidbody rb;
    public float speedMultiplier;
    public float rotationSpeed;
    public int currentRotation = 0;
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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) currentRotation -= 90;
        if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow)) currentRotation += 90;
        //if (currentRotation == transform.rotation.eulerAngles.y && currentRotation < 0)
        //{
        //    currentRotation *= -1;
        //}
        //rb.AddRelativeTorque(0,Input.GetAxis("Horizontal") * rotationSpeed,0,ForceMode.VelocityChange);
        //Quaternion test = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles,new Vector3(0,currentRotation,0),Time.deltaTime /rotationSpeed));
        Quaternion test = Quaternion.Euler(0,currentRotation,0);
        transform.rotation = Quaternion.Lerp(transform.rotation,test,Time.deltaTime *rotationSpeed);
    } 
}
