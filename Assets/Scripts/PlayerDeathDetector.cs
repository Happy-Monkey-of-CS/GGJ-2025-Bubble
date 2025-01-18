using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Killer"))
        {
            GameManager.Instance.GameOver();
        }
        if (collider.CompareTag("Point"))
        {
            //Destroy(collider.transform.parent.gameObject);
            StartCoroutine(Absorb(collider.transform.parent.gameObject.GetComponent<Rigidbody2D>()));
        }
    }

    IEnumerator Absorb(Rigidbody2D go)
    {
        float t = 0;
        while (t < 0.3f)
        {
            t+=Time.deltaTime;
            go.AddForce(Time.deltaTime * (transform.position - go.transform.position).normalized*1000);
            yield return null;
        }
        Destroy(go.gameObject);
    }
}
