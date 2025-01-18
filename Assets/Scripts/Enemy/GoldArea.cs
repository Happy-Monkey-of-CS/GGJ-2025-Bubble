using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldArea : MonoBehaviour
{ public float velocity;
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
        sprite = GetComponent<SpriteRenderer>();
    }

    public float delay=0.5f;
    public float stayTime=3f;

    void Update()
    {
        if (mother == null)
        {DestroySlowly();
            return;
        }
        transform.position = mother.position;
        transform.eulerAngles =mother.eulerAngles;

        delay-=Time.deltaTime;
        if(delay>0){
            return;
        }

        //sprite.color = new Color(1, 1, 1, 0.5f + (BiggestSize - transform.localScale.x) / BiggestSize / 2);

        if (transform.localScale.x > BiggestSize)
        {
        transform.localScale += Vector3.one * BiggerSpeed * Time.deltaTime*0.05f;
        }else{
        transform.localScale += Vector3.one * BiggerSpeed * Time.deltaTime;
        }

        if(delay<-stayTime){
        sprite.color = new Color(1, 1, 1, 1+(delay+stayTime)*2);
        }
        if(delay<-stayTime-0.5f){
            Destroy(gameObject);
        }
    }

    public void DestroySlowly()
    {
        StartCoroutine(Des());
    }

    IEnumerator Des()
    {
        float t = 0;
        while (t < 0.5)
        {
            t += Time.deltaTime;
            sprite.color = new(1 - t, 1 - t, 1 - t,0.5f-t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
