using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timmer : MonoBehaviour
{
    public  TextMesh text;
    public string fwdS,aftS;

    float startTime;

    void Start(){
        startTime=Time.time;
    }
    
    void Update()
    {
        text.text=fwdS+(Time.time-startTime).ToString("F2")+aftS;
    }
}
