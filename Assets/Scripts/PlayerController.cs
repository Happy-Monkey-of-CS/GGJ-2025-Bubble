using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float direction;            //当前移动方向，0不动，1左2右3上4下5左上6右上7左下8右下
    float timer;
    float energy;
    public float speed = 50f;         //运动速度
    public float max_time = 2.0f;
    public float speed_energy = 2.0f;   // 用一次空格耗能
    public float max_energy = 50f;      // 能量槽大小

    public MusicManager music;
    // Start is called before the first frame update
    void Start()
    {
        music = MusicManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        direction = 0;
        timer = 0;
        energy = 200;
    }

    private float GetDirection(){
        float result = 0;
        if(music.IsOnBeat() || Input.GetKey(KeyCode.Space)){
            if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)){
                result = 5;
            }
            else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)){
                result = 6;
            }
            else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)){
                result = 7;
            }
            else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)){
                result = 8;
            }
            else{
                if (Input.GetKey(KeyCode.A))
                {
                    result = 1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    result = 2;
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    result = 3;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    result = 4;
                }
            }
            timer = 0;
        }
        return result;
    }

    private void FixedUpdate()
    {
        direction = GetDirection();
        if (direction != 0)
        {
            Vector2 position = rb.position;
            if(direction == 1){
                position.x -= speed * Time.deltaTime;
            }
            else if(direction == 2){
                position.x += speed * Time.deltaTime;
            }
            else if(direction == 3){
                position.y += speed * Time.deltaTime;
            }
            else if(direction == 4){
                position.y -= speed * Time.deltaTime;
            }
            else if(direction == 5){
                position.x -= Mathf.Pow(speed, 0.5f) * Time.deltaTime;
                position.y += Mathf.Pow(speed, 0.5f) * Time.deltaTime;
            }
            else if(direction == 6){
                position.x += Mathf.Pow(speed, 0.5f) * Time.deltaTime;
                position.y += Mathf.Pow(speed, 0.5f) * Time.deltaTime;
            }
            else if(direction == 7){
                position.x -= Mathf.Pow(speed, 0.5f) * Time.deltaTime;
                position.y -= Mathf.Pow(speed, 0.5f) * Time.deltaTime;
            }
            else if(direction == 8){
                position.x += Mathf.Pow(speed, 0.5f) * Time.deltaTime;
                position.y -= Mathf.Pow(speed, 0.5f) * Time.deltaTime;
            }
            rb.MovePosition(position);
            timer += Time.deltaTime;
            if(timer >= max_time){
                timer = 0;
                direction = 0;
            }
        }
    }
}
