using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance = null;
    AudioSource source;
    public AudioClip button;
    public AudioClip fail;
    public AudioClip beat;
    public AudioClip start;


    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
        source = gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayButton(){
        source.clip = button;
        source.Play();
    }
    public void PlayFail(){
        source.clip = fail;
        source.Play();
    }
    public void PlayBeat(){
        source.clip = beat;
        source.Play();
    }
    public void PlayStart(){
        source.clip = start;
        source.Play();
    }
}
