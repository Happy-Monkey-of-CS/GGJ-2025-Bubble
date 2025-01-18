using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    Rigidbody2D rb;

    public float force;

    public void Change(){

        int randomValue = Random.Range(0, 2) == 0 ? -1 : 1;
        force*=randomValue;
    }
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(force);
    }
}
