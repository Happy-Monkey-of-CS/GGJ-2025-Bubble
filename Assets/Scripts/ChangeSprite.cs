using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public SpriteData[] datas;

    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        MusicManager.Instance.OnChange += Change;
    }

    void Change(string s)
    {
        foreach (var b in datas)
        {
            if (s.Contains(b.name))
            {
                spriteRenderer.sprite = b.sprite;
            }
        }
    }
}

[Serializable]
public struct SpriteData
{
    public string name;
    public Sprite sprite;
}
