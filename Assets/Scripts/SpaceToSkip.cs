using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceToSkip : MonoBehaviour
{
    public GameObject go;
    void Update()
    {
        if(Input.anyKeyDown){
            go.SetActive(true);
        }
    }
}
