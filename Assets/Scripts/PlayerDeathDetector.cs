using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Killer")){
        GameManager.Instance.GameOver();
        }
    }
}
