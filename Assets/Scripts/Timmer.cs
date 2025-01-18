using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timmer : MonoBehaviour
{
    public TextMesh text;
    public TextMesh text2;
    [TextArea]
    public string fwdS, aftS,second;
    string player_name;

    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    public void Awake()
    {
        player_name = PlayerPrefs.GetString("Name");
    }

    void Update()
    {
        text.text = player_name + fwdS + (Time.time - startTime).ToString("F2") + aftS;
        text2.text = "";
        for (int x = 0; x < (Time.time - startTime).ToString("F2").Length-3; x+=1)
        {
            text2.text += " ";
        }
        text2.text += second;
    }

    public float GetTime()
    {
        return Time.time - startTime;
    }
}
