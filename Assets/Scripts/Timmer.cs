using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timmer : MonoBehaviour
{
    public  TextMesh text;
    public string fwdS,aftS;
    string name;

    float startTime;

    void Start(){
        startTime=Time.time;
    }

    public void Awake(){
        name = PlayerPrefs.GetString("Name");
    }
    
    void Update()
    {
        text.text=name+fwdS+(Time.time-startTime).ToString("F2")+aftS;
    }

    public float GetTime(){
        return Time.time-startTime;
    }
}
