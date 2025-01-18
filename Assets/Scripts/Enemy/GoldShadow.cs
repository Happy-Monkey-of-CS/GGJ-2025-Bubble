using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldShadow : MonoBehaviour
{
    SpriteRenderer sprite;
    float timmer=0;

    void Start(){
        timmer=0;
        sprite=GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        timmer+=Time.deltaTime/2;
        if(timmer<0.5f){
            sprite.color=new Color(0,0,0,timmer);
        }
    }
}
