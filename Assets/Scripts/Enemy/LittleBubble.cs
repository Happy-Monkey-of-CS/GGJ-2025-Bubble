using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBubble : Enemy
{
    public Sprite[] sprites;

    SpriteRenderer sr;

    public int id;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
        int randomIndex = Random.Range(0, sprites.Length);
        id=randomIndex;
        sr.sprite = sprites[id];
    }

}
