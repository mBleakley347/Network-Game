using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private int speed;

    [SerializeField] private Rigidbody rb;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (life > 0)rb.AddRelativeForce(Vector3.forward * speed,ForceMode.VelocityChange);
        else Destroy(gameObject);
        life--;
    }
}
