using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    public AudioSource levelAudio;
    public float updateStep = 0.01f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    public float clipLoudness = 0f;
    private float[] clipSampleData;

    public float sizeFactor = 10f;

    public float minSize = 10f;
    public float maxSize = 25f;

    private void Awake()
    {
        clipSampleData = new float[sampleDataLength];
    }

    private void Update()
    {
        if (levelAudio.isPlaying && PlayerPrefs.GetInt("Ring", 1) == 1)
        {
            currentUpdateTime += Time.deltaTime;

            if (currentUpdateTime >= updateStep)
            {
                currentUpdateTime = 0f;
                levelAudio.clip.GetData(clipSampleData, levelAudio.timeSamples);
                clipLoudness = 0f;
                
                foreach (var sample in clipSampleData)
                {
                    clipLoudness += Mathf.Abs(sample);
                }
                
                clipLoudness /= sampleDataLength;
                clipLoudness *= sizeFactor;
                clipLoudness = Mathf.Clamp(clipLoudness, 0, (maxSize - minSize));
                clipLoudness += minSize;

                transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
            }
        }
    }
}
