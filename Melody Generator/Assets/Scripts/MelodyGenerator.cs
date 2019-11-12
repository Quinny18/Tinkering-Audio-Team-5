using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://pages.mtu.edu/~suits/notefreqs.html

public class MelodyGenerator : MonoBehaviour
{
    int sampleDuration = 5;
    float sampleAmplitude = 131.87f;
    int sampleLength = sampleAmplitude * sampleDuration;
    float maxValue = 1f / 4f;
    //C4
    float frequency = 261.63f;

    //Base Tone
    private AudioClip CreateBaseToneAudioClip()
    {
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleAmplitude, true);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(4.0f * Mathf.PI * frequency * ((float) i / (float)sampleAmplitude));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

    private AudioClip CreateToneAudioClip()
    {
        //A4
        var clip = AudioClip.Create("high", sampleLength, 5, sampleAmplitude - 53.46, true);


        float[] highSamples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float j = Mathf.Sin(2.0f * Mathf.PI * (frequency = 392.00) * ((float)i / (float)sampleAmplitude));
            float k = j * maxValue;
            highSamples[i] = k;
        }

        clip.SetData(highSamples, 0);
        return clip;
    }

    //button to play the audio source
    var clicked = CreateBaseToneAudioClip() + CreateToneAudioClip();



}
