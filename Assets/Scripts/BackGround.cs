using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform bg;
public Transform player;

    public Color color;

    void OnEnable()
    {
        Camera.main.backgroundColor=color;
        Vector3 targetPos = player.position  *0.1f;
        bg.position = targetPos;
    }

    public float smoothTime;

    private Vector3 velocity;

    void Update()
    {

        Vector3 targetPos = player.position  *0.1f;

        bg.position = Vector3.SmoothDamp(bg.position, targetPos, ref velocity, smoothTime);
    }
}
