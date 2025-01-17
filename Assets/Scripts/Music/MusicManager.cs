using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // ����
    public static MusicManager Instance { get; private set; }
    public MusicManager()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Replacing existing instance");
        }
        Instance = this;
    }

    // ����ʵ��
    public Music music { get; private set; }

    // �л�����
    public void SwitchTo(string name)
    {
        if(music!=null){
        Destroy(music.gameObject);
        }
        try
        {
            GameObject go = Resources.Load<GameObject>($"music/{name}");
            Music m = Instantiate(go,transform).GetComponent<Music>();
            m.gameObject.transform.localPosition=Vector3.zero;
            music = m;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    // ����Ƿ�������(һ��ֻ����һ��true)
    public bool IsOnBeat()
    {
        if(music==null){
            return false;
        }
        return music.IsOnBeat();
    }

    // ���õ�ǰ����Ϊ����ɵ�����
    public void FinishedBeat(){
        music.FinishedBeat();
    }

    public string StartMusicName="NewYear";

    //private
    private void Awake()
    {
        SwitchTo(StartMusicName);
    }
}
