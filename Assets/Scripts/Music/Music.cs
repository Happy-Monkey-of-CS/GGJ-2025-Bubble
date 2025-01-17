using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource audioSource; // 绑定的音频源
    public float bpm = 120f; // 每分钟节拍数

    public int beatCount = 4;

    public int displayCount = 3;

    public float offset = 0f; // 开头的偏移时间（秒）

    public GameObject beatDisplay;

    public Vector3 scale;

    List<BeatDisplay> beats = new();

    int lastBeat = 0;

    int lastSuccessBeat = -1;

    public float error;

    public bool IsOnBeat()
    {
        float elapsedTime = audioSource.time - offset;
        float currentBeat = elapsedTime * bpm / 60;
        int intBeat = Mathf.RoundToInt(currentBeat);
        if (Mathf.Abs(currentBeat-intBeat)<error&& intBeat % beatCount == 0&&intBeat!=lastSuccessBeat)
        {
            lastSuccessBeat = intBeat;
            return true;
        }
        return false;
    }

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource 未绑定！");
            return;
        }

        // 开始播放音频并记录开始时间
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            // 当前音频播放的总时间（包括偏移）
            float elapsedTime = audioSource.time - offset;

            // 当前是第几拍（从1开始）
            float currentBeat = elapsedTime * bpm / 60;

            // 打印当前进度信息

            // Debug.Log($"当前时间: {elapsedTime:F2}s，第 {currentBeat} 拍");

            while (lastBeat < currentBeat + beatCount * displayCount)
            {
                lastBeat += beatCount;
                GameObject go = Instantiate(beatDisplay, transform);
                BeatDisplay beat = go.GetComponent<BeatDisplay>();
                beat.Init(bpm, offset, scale, lastBeat);
                beats.Add(beat);

                GameObject go2 = Instantiate(beatDisplay, transform);
                BeatDisplay beat2 = go2.GetComponent<BeatDisplay>();
                Vector3 s2 = scale;
                s2.x *= -1;
                beat2.Init(bpm, offset, s2, lastBeat);
                beats.Add(beat2);
            }

            foreach (var b in beats.ToArray())
            {
                if (b.ChangePos(audioSource.time))
                {
                    beats.Remove(b);
                    Destroy(b.gameObject);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)&& IsOnBeat()){
            Debug.Log(1);
        }
    }
}
