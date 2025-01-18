using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float force;
    public float torque;
    public float spawnR = 1;

    Vector3 startPoint;

    Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomAngle = Random.Range(0f, 360f);
        transform.localEulerAngles = new Vector3(0, 0, randomAngle);
        int randomValue = Random.Range(0, 2) == 0 ? -1 : 1;
        //float randomValue = Random.Range(-1f, 1f);
        torque *= randomValue;
    }

    protected virtual void Start()
    {
        startPoint = transform.position;
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

    protected virtual void Update()
    {
        if (Distance(Camera.main.transform.position, startPoint) > 10 && Distance(Camera.main.transform.position, transform.position) > 10)
        {
            transform.position = startPoint;
        }
    }

    float Distance(Vector3 a, Vector3 b)
    {
        a.z = 0;
        b.z = 0;
        return (a - b).magnitude;
    }
}
