using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public BGData[] datas;

    void Awake()
    {
        MusicManager.Instance.OnChange += Change;
    }

    void Change(string s)
    {
        foreach (var b in datas)
        {
            b.go.SetActive(false);
        }
        foreach (var b in datas)
        {
            if (s == b.name)
            {
                b.go.SetActive(true);
            }
        }
    }
}

[Serializable]
public class BGData
{
    public string name;
    public GameObject go;
}
