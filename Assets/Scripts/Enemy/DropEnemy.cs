using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEnemy : Enemy
{
    public float startSize;
    public Collider2D[] cs;

    public SpriteRenderer[] sprites;

    protected override void Awake()
    {
        base.Awake();
        foreach (var c in cs)
        {
            c.enabled = false;
        }
        foreach (var s in sprites)
        {
            s.color = new(1, 1, 1, 0);
        }
        transform.localScale = Vector3.one * (Mathf.Cos(Mathf.PI * 0) / (1f + Mathf.Pow(0, 3)) / 2 + 1);
    }

    protected enum State
    {
        waiting,
        dropping,
        floating,
        stop,
    }

    protected State state = State.waiting;

    public float timmer = 0;
    protected virtual void Update()
    {
        switch (state)
        {
            case State.waiting:
                timmer += Time.deltaTime;
                if (timmer >= 0)
                {
                    timmer = 0;
                    state = State.dropping;
                }
                break;
            case State.dropping:
                timmer += Time.deltaTime;
                transform.localScale = Vector3.one * (Mathf.Cos(Mathf.PI * timmer) / (1f + Mathf.Pow(timmer, 3)) / 2 + 1);
                foreach (var s in sprites)
                {
                    s.color = new(0.2f + timmer / 0.5f * 0.8f, 0.2f + timmer / 0.5f * 0.8f, 0.2f + timmer / 0.5f * 0.8f);
                }
                if (timmer > 0.5f)
                {
                    state = State.floating;
                    foreach (var c in cs)
                    {
                        c.enabled = true;
                    }
                    foreach (var s in sprites)
                    {
                        s.color = new(1, 1, 1);
                    }
                }
                break;
            case State.floating:
                timmer += Time.deltaTime;
                transform.localScale = Vector3.one * (Mathf.Cos(Mathf.PI * timmer) / (1f + Mathf.Pow(timmer + 0.5f, 3)) / 2 + 1);
                if (timmer > 4.5f)
                {
                    state = State.stop;
                }
                break;
            case State.stop:
                transform.localScale = Vector3.one;
                break;
        }
    }

    public override void DestroySlowly()
    {
        StartCoroutine(Des());
    }

    IEnumerator Des()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            if (sprites == null)
            {
                break;
            }
            foreach (var s in sprites)
            {
                if (s != null)
                {
                    s.color = new(1 - t, 1 - t, 1 - t, 1 - t);
                }
            }
            yield return null;
        }
        Destroy(gameObject);
    }
}
