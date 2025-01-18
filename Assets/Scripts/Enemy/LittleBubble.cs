using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBubble : Enemy
{
    public Sprite[] sprites;

    SpriteRenderer sr;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = GetRandomElement(sprites);
    }



    T GetRandomElement<T>(T[] list)
    {
        int randomIndex = Random.Range(0, list.Length);
        return list[randomIndex];
    }
}
