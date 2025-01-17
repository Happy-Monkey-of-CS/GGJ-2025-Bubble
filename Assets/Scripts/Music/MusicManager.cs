using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // 单例
    public MusicManager Instance { get; private set; }
    public MusicManager()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Replacing existing instance");
        }
        Instance = this;
    }

    // 音乐实例
    public Music music { get; private set; }

    // 切换音乐
    public void SwitchTo(string name)
    {
        if(music!=null){
        Destroy(music.gameObject);
        }
        try
        {
            GameObject go = Resources.Load<GameObject>($"music/{name}");
            Music m = Instantiate(go).GetComponent<Music>();
            music = m;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    // 检测是否在拍上(一拍只返回一次true)
    public bool IsOnBeat()
    {
        if(music==null){
            return false;
        }
        return music.IsOnBeat();
    }

    public string StartMusicName="NewYear";

    //private
    private void Awake()
    {
        SwitchTo(StartMusicName);
    }
}
