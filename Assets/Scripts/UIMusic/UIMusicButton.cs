using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMusicButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(PlayMusic);
    }

    // Update is called once per frame
    void PlayMusic()
    {
        GameObject.Find("DontDestory").GetComponent<DontDestroy>().PlayButton();
    }
}
