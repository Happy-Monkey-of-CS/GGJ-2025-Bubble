using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDirection : MonoBehaviour
{
    public void Reset(){
        transform.parent.transform.up = new Vector2(0,1);
    }
}
