using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusing : MonoBehaviour
{
    public Rigidbody2D target;

    public float distance;

    public float minSpeed;

    public float smoothTime;

    private Vector2 direction;
    private Vector3 velocity;

    private void Update()
    {
        if (target.velocity.magnitude > minSpeed)
        {
            direction = target.velocity.normalized;
        }

        Vector3 targetPos = target.transform.position + new Vector3(direction.x, direction.y, 0) * distance;

        targetPos.z = -10;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

}
