using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDisplay : MonoBehaviour
{
    public float bpm;

    public float offset;

    public int beatCount;

    public Vector3 scale;

    public void Init(float bpm, float offset,Vector3 scale, int beatCount)
    {
        this.bpm = bpm;
        this.offset = offset;
        this.beatCount = beatCount;
        this.scale=scale;
    }

    public bool ChangePos(float time)
    {
        float elapsedTime = time - offset;
        float currentBeat = elapsedTime * bpm / 60f;
        if (beatCount < currentBeat)
        {
            return true;
        }
        else
        {
            transform.localPosition = scale * (beatCount - currentBeat);
            return false;
        }
    }
}
