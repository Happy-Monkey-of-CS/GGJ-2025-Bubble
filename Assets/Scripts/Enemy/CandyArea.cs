using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyArea : MonoBehaviour
{
    public float velocity;
    public float BiggerSpeed;
    public float BiggestSize;
    Transform mother;

    SpriteRenderer sprite;
    void Start()
    {
        mother = transform.parent;
        transform.SetParent(transform.parent.parent);
        int randomValue = Random.Range(0, 2) == 0 ? -1 : 1;
        velocity *= randomValue;
        sprite=GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = mother.position;
        transform.localEulerAngles += new Vector3(0, 0, velocity * Time.deltaTime);

        sprite.color=new Color(1,1,1,0.5f+(BiggestSize-transform.localScale.x)/BiggestSize/2);

        transform.localScale += Vector3.one * BiggerSpeed * Time.deltaTime;
        if (transform.localScale.x > BiggestSize)
        {
            transform.localScale = Vector3.one * BiggestSize;
        }
    }
}
