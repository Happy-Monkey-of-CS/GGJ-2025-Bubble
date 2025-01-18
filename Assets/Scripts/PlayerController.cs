using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    int bubble_num;
    int bubble_max = 10;         // 最大容量
    public float force = 50f;         //运动速度

    public float speedUpRate = 2;

    public float inputDelay = 0.1f; // 斜向输入延迟

    public Animator animator;

    GameObject bubbles;
    public GameObject[] prefabs;        // 1蓝2粉3紫4黄

    MusicManager music;
    void Start()
    {
        music = MusicManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        playerRotation=GetComponent<PlayerRotation>();
        bubbles = GameObject.Find("Bubbles").gameObject;
    }

    public KeyCode up, down, left, right, speedUp;

    Vector2 inputBuffer;

    float inputBufferTimmer = 0;
    bool doubleInput = true;

    PlayerRotation playerRotation;


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
                //transform.up=d;
                transform.localEulerAngles=new Vector3(0,0,-Mathf.Atan2(d.x,d.y)*Mathf.Rad2Deg);
                music.FinishedBeat();
                if (Input.GetKey(speedUp) && bubble_num > 0)
                {
                    RemoveBubble();
                    d *= speedUpRate;
                }

                Collider2D[] colliders=Physics2D.OverlapCircleAll(transform.position,0.5f,LayerMask.GetMask("Player"));

                foreach(var c in colliders){
                    if(c.CompareTag("LowSpeed")){
                        d/=speedUpRate;
                    }
                }

                rb.AddForce(d * force);
                animator.SetTrigger("Move");
                // 播放拍子声音
                //GameObject.Find("DontDestory").GetComponent<DontDestroy>().PlayBeat();
                rb.angularVelocity=0;

                playerRotation.Change();

                inputBufferTimmer = 0;
                inputBuffer = Vector2.zero;
                doubleInput = true;
            }
        }


    }

    public void AddBubble(int index){
        if(bubble_num < bubble_max){
            bubble_num++;
            GameObject bubble = GameObject.Instantiate(prefabs[index]) as GameObject;
            bubble.transform.parent = bubbles.transform;
            bubble.transform.position = GameObject.Find("Brith").gameObject.transform.position;
        }
    }

    public void RemoveBubble(){
        if(bubble_num > 0){
            bubble_num--;
            Destroy(bubbles.transform.GetChild(0).gameObject);
        }
    }

    private void Update()
    {
        GetInput();
        return;
    }
}
