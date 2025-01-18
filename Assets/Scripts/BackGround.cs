using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    
    public Color color;

    void OnEnable()
    {
        Camera.main.backgroundColor=color;
    }
}
