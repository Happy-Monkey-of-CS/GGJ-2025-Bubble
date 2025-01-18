using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultName : MonoBehaviour
{
    public string defaultName;

    void Start()
    {
            PlayerPrefs.SetString("Name", defaultName);
    }
}
