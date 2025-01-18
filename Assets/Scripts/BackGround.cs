using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform fwd,bkwd;
public Transform player;

    public Color color;

    void OnEnable()
    {
        Camera.main.backgroundColor=color;
    }

    public float smoothTime;

    private Vector3 velocity;

    void Update()
    {

        Vector3 targetPos = player.position  *0.1f;

        bkwd.position = Vector3.SmoothDamp(bkwd.position, targetPos, ref velocity, smoothTime);
    }
}
