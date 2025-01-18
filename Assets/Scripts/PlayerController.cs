using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public int energy;
    public float force = 50f;         //运动速度
    public int speed_energy = 1;   // 用一次空格耗能

    public float speedUpRate = 2;

    public float inputDelay = 0.1f; // 斜向输入延迟

    MusicManager music;
    void Start()
    {
        music = MusicManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        energy = 200;
    }

    public KeyCode up, down, left, right, speedUp;

    Vector2 inputBuffer;

    float inputBufferTimmer = 0;
    bool doubleInput = true;

    private void GetInput()
    {
        if (inputBuffer == Vector2.zero) //第一次输入
        {
            if (music.IsOnBeat())
            {
                if (Input.GetKeyDown(up) && !Input.GetKeyDown(down))
                {
                    inputBuffer = Vector2.up;
                }
                else if (Input.GetKeyDown(down) && !Input.GetKeyDown(up))
                {
                    inputBuffer = Vector2.down;
                }
                else if (Input.GetKeyDown(left) && !Input.GetKeyDown(right))
                {
                    inputBuffer = Vector2.left;
                }
                else if (Input.GetKeyDown(right) && !Input.GetKeyDown(left))
                {
                    inputBuffer = Vector2.right;
                }
            }
            if (inputBuffer != Vector2.zero)
            {
                inputBufferTimmer = 0;
                doubleInput = true;
                GetInput();
            }
        }
        else // 斜向输入
        {
            inputBufferTimmer += Time.deltaTime;

            if (doubleInput)
            {
                doubleInput = false;
                if (Input.GetKeyDown(up) && !Input.GetKeyDown(down) && inputBuffer != Vector2.up && inputBuffer != Vector2.down)
                {
                    inputBuffer += Vector2.up;
                }
                else if (Input.GetKeyDown(down) && !Input.GetKeyDown(up) && inputBuffer != Vector2.up && inputBuffer != Vector2.down)
                {
                    inputBuffer += Vector2.down;
                }
                else if (Input.GetKeyDown(left) && !Input.GetKeyDown(right) && inputBuffer != Vector2.left && inputBuffer != Vector2.right)
                {
                    inputBuffer += Vector2.left;
                }
                else if (Input.GetKeyDown(right) && !Input.GetKeyDown(left) && inputBuffer != Vector2.left && inputBuffer != Vector2.right)
                {
                    inputBuffer += Vector2.right;
                }
                else
                {
                    doubleInput = true;
                }
            }

            if (inputBufferTimmer >= inputDelay || !doubleInput)
            {
                inputBuffer = inputBuffer.normalized;
                Vector2 d = inputBuffer;
                transform.up=d;
                music.FinishedBeat();
                if (Input.GetKey(speedUp) && energy > speed_energy)
                {
                    energy -= speed_energy;
                    d *= speedUpRate;
                }

                Collider2D[] colliders=Physics2D.OverlapCircleAll(transform.position,0.5f,LayerMask.GetMask("Player"));

                foreach(var c in colliders){
                    if(c.CompareTag("LowSpeed")){
                        d/=speedUpRate;
                    }
                }

                rb.AddForce(d * force);

                inputBufferTimmer = 0;
                inputBuffer = Vector2.zero;
                doubleInput = true;
            }
        }


    }

    private void Update()
    {
        GetInput();
        return;
    }
}
