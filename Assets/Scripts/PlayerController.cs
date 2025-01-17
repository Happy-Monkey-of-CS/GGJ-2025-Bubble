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

    MusicManager music;
    void Start()
    {
        music = MusicManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        energy = 200;
    }

    public KeyCode up, down, left, right, speedUp;

    private void GetInput()
    {
        Vector2 d = Vector2.zero;
        if (Input.GetKeyDown(up) && !Input.GetKeyDown(down))
        {
            d = Vector2.up;
        }
        else if (Input.GetKeyDown(down) && !Input.GetKeyDown(up))
        {
            d = Vector2.down;
        }
        else if (Input.GetKeyDown(left) && !Input.GetKeyDown(right))
        {
            d = Vector2.left;
        }
        else if (Input.GetKeyDown(right) && !Input.GetKeyDown(left))
        {
            d = Vector2.right;
        }

        if (d != Vector2.zero && music.IsOnBeat())
        {
            music.FinishedBeat();
            if (Input.GetKey(speedUp) && energy > speed_energy)
            {
                energy -= speed_energy;
                d *= speedUpRate;
            }
            rb.AddForce(d * force);
        }
    }
    private void Update()
    {
        GetInput();
        return;
    }
}
