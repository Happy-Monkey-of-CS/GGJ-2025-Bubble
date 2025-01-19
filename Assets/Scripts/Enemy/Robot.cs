using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{
    Rigidbody2D rb2;
    Vector3 direction;
    protected override void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        transform.position = GetInitPosition();
        direction = Camera.main.transform.position - transform.position;
        direction.z = 0;
    }

    protected override void FixedUpdate()
    {
        rb2.AddForce(force * direction);
        rb2.AddTorque(torque);
    }

    Vector3 GetInitPosition(){
        Vector3 camera = Camera.main.transform.position;
        float DisX = Random.Range(10f, 11f);
        float DisY = Random.Range(6f, 7f);
        Vector3 result = camera;
        result.z = 0;
        float p = Random.Range(0, 10f);
        result.x = p > 5f ? result.x + DisX : result.x - DisX;
        p = Random.Range(0, 10f);
        result.y = p > 5f ? result.y + DisY : result.y - DisY;
        return result;
    }
}
