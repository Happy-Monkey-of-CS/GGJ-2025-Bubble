using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Enemy
{
    Rigidbody2D rb2;
    protected override void Awake()
    {
        base.Awake();
        MusicManager.Instance.OnBeat += OnBeat;
        rb2 = GetComponent<Rigidbody2D>();
    }

    public float dampForce;

    float lastTime = 0;
    public float maxSpeed = 1;

    protected override void FixedUpdate()
    {
        if (rb2.velocity.magnitude > maxSpeed)
        {
            rb2.velocity *= 0.9f;
        }

            rb2.AddRelativeForce(force*Vector2.up );
    }

    void OnBeat()
    {
        if (Time.time - lastTime > 0.1f)
        {
            int randomValue = Random.Range(0, 2) == 0 ? -1 : 1;
            StartCoroutine(Force(Vector2.up * dampForce, torque * randomValue));
        }
        lastTime = Time.time;
    }

    IEnumerator Force(Vector2 f, float t)
    {
        int time = 0;
        while (time < 20)
        {
            time += 1;
            yield return new WaitForFixedUpdate();
            rb2.AddRelativeForce(f);
            rb2.AddTorque(t);
        }
    }
}
