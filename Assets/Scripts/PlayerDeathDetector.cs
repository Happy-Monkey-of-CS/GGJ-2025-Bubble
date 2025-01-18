using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathDetector : MonoBehaviour
{
    void OnTriggerEnter2D(){
        GameManager.Instance.GameOver();
    }
}
