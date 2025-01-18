using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float force;
    public float torque;
    public float spawnR = 1;

    Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomAngle = Random.Range(0f, 360f);
        transform.localEulerAngles = new Vector3(0, 0, randomAngle);
        //int randomValue = Random.Range(0, 2) == 0 ? -1 : 1;
        float randomValue = Random.Range(-1f, 1f);
        torque *= randomValue;
    }

    protected virtual void FixedUpdate()
    {
        rb.AddForce(force * transform.up);
        rb.AddTorque(torque);
    }
    public virtual void DestroySlowly()
    {
        Destroy(gameObject);
    }
}
