using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource audioSource; // �󶨵���ƵԴ
    public float bpm = 120f; // ÿ���ӽ�����

    public int beatCount = 4;

    public int displayCount = 3;

    public float offset = 0f; // ��ͷ��ƫ��ʱ�䣨�룩

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
            Debug.LogError("AudioSource δ�󶨣�");
            return;
        }

        // ��ʼ������Ƶ����¼��ʼʱ��
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            // ��ǰ��Ƶ���ŵ���ʱ�䣨����ƫ�ƣ�
            float elapsedTime = audioSource.time - offset;

            // ��ǰ�ǵڼ��ģ���1��ʼ��
            float currentBeat = elapsedTime * bpm / 60;

            // ��ӡ��ǰ������Ϣ

            // Debug.Log($"��ǰʱ��: {elapsedTime:F2}s���� {currentBeat} ��");

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
