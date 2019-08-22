using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    [SerializeField] public float life = 0f;
    [SerializeField] private int speed = 0;

    [SerializeField] private Rigidbody rb = null;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (life > 0)rb.AddRelativeForce(Vector3.forward * speed,ForceMode.VelocityChange);
        else Destroy(gameObject);
        life-= Time.deltaTime;
    }
}
