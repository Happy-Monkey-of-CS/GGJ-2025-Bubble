using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public float time;
    public string scene;
    float timer = 0f;

    void Update(){
        timer += Time.deltaTime;
        if(timer > time || Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene(scene);
        }
    }
}
