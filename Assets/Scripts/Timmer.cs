using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timmer : MonoBehaviour
{
    public  TextMesh text;
    [TextArea]
    public string fwdS,aftS;
    string player_name;

    float startTime;

    void Start(){
        startTime=Time.time;
    }

    public void Awake(){
        player_name = PlayerPrefs.GetString("Name");
    }
    
    void Update()
    {
        text.text=player_name+fwdS+(Time.time-startTime).ToString("F2")+aftS;
    }

    public float GetTime(){
        return Time.time-startTime;
    }
}
