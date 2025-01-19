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

    public string[] musics;

    // ����ʵ��
    public Music music { get; private set; }

    // �л�����
    public void SwitchTo(string name)
    {
        lastSecond = 0;
        if (music != null)
        {
            Destroy(music.gameObject);
        }
        try
        {
            GameObject go = Resources.Load<GameObject>($"music/{name}");
            Music m = Instantiate(go, transform).GetComponent<Music>();
            m.gameObject.transform.localPosition = Vector3.zero;
            music = m;
            music.OnBeat+=Onbeat;

            OnChange?.Invoke(name);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    void Onbeat(){
        OnBeat?.Invoke();
    }

    // ����Ƿ�������(һ��ֻ����һ��true)
    public bool IsOnBeat()
    {
        if (music == null)
        {
            return false;
        }
        return music.IsOnBeat();
    }

    // ���õ�ǰ����Ϊ����ɵ�����
    public void FinishedBeat()
    {
        music.FinishedBeat();
    }

    public string StartMusicName = "NewYear";

    public Action<string> OnChange;

    public Action OnBeat;

    //private
    private void Start()
    {
        StartMusicName = GetRandomElement(musics);

        SwitchTo(StartMusicName);
        nowPlaying = StartMusicName;
    }

    public string nowPlaying{get;private set;}= "";
    int lastSecond = 0;

    private void Update()
    {
        if (!music.audioSource.isPlaying)
        {
            RandomMusic();
        }
        else
        {
            if (music.audioSource.time >=10)
            {
                if (lastSecond != Mathf.FloorToInt(music.audioSource.time))
                {
                    lastSecond = Mathf.FloorToInt(music.audioSource.time);
                    if (UnityEngine.Random.Range(0,1f) < 0.1f)
                    {
                        RandomMusic();
                    }
                }
            }
        }
    }

    public void RandomMusic()
    {
        string n = GetRandomElement(musics);
        while (n == nowPlaying)
        {
            n = GetRandomElement(musics);
        }
        SwitchTo(n);
        nowPlaying = n;
    }
    T GetRandomElement<T>(T[] list)
    {
        int randomIndex = UnityEngine.Random.Range(0, list.Length);
        return list[randomIndex];
    }
}
