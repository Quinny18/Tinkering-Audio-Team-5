using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MelodyGenerator : MonoBehaviour
{
    //declaring variables for use in functions
    private AudioSource Melody;
    private AudioClip outAudioClip;

    private float frequency;

    private int sampleDuration = 10;

    private float baseFrequency;
    private float mainFrequency;

    private int chosenFrequencyB;
    private int chosenFrequencyM;

    private double add;

    private float gain;
    private float volume = 0.2f;

    private void Start()
    {
        MakeMelody();

    }

    public void MakeMelody()
    {
        //creating an array to store frequencies
        var notesFrequency = new List<float> {
            261.63f, //C4
            293.66f, //D4
            329.63f, // E4
            349.23f, //F4
            392.00f, //G4
            440.00f, //A4
            493.88f, //B4
            277.18f, //C#4
            311.13f, //D#4
            369.99f, //F#4
            415.30f, //G#4
            466.16f, //A#4
            16.35f, //C0
            18.35f, //D0
            20.60f, //E0
            21.83f, //F0
            24.50f, //G0
            27.50f, //A0
            30.87f, //B0
            17.32f, //C#0
            19.45f, //D#0
            23.12f, //F#0
            25.96f, //G#0
            29.14f, //A#0
            32.70f, //C1
            36.71f, //D1
            41.20f, //E1
            43.65f, //F1
            49.00f, //G1
            55.00f, //A1
            61.74f, //B1
            34.65f, //C#1
            38.89f, //D#1
            46.25f, //F#1
            51.91f, //G#1
            58.27f, //A#1
            65.41f, //C2
            73.42f, //D2
            82.41f, //E2
            87.32f, //F2
            98.00f, //G2
            110.00f, //A2
            123.47f, //B2
            69.30f, //C#2
            77.78f, //D#2
            92.50f, //F#2 
            103.83f, //G#2
            116.54f, //A#2
            130.81f, //C3
            146.83f, //D3
            164.81f, //E3
            174.61f, //F3
            196.00f, //G3
            220.00f, //A3
            246.94f, //B3
            138.59f, //C#3
            155.56f, //D#3
            185.00f, //F#3
            207.65f, //G#3
            233.08f, //A#3
            523.25f, //C5
            587.33f, //D5
            659.25f, //E5
            698.46f, //F5
            783.99f, //G5
            880.00f, //A5
            987.77f, //B5 
            554.37f, //C#5
            622.25f, //D#5
            739.99f, //F#5
            830.61f, //G#5
            932.33f, //A#5
            1046.50f, //C6
            1174.66f, //D6
            1318.51f, //E6 
            1396.91f, //F6
            1567.98f, //G6
            1760.00f, //A6
            1975.53f, //B6
            1108.73f, //C#6
            1244.51f, //D#6
            1479.98f, //F#6
            1567.98f, //G#6
            1864.66f, //A#6
            2093.00f, //C7
            2349.32f, //D7
            2637.02f, //E7
            2793.83f, //F7
            3135.96f, //G7
            3530.00f, //A7
            3951.07f, //B7
            2217.46f, //C#7
            2489.02f, //D#7
            2959.96f, //F#7
            3322.44f, //G#7
            3729.31f, //A#7
            4186.01f, //C8
            4698.63f, //D8
            5274.04f, //E8
            5587.65f, //F8
            6271.93f, //G8
            7040.00f, //A8
            7902.13f, //B8
            4434.92f, //C#8
            4978.03f, //D#8
            5919.91f, //F#8
            6644.88f, //G#8
            1458.62f, //A#8
        };

    
        Melody = GetComponent<AudioSource>();

        var counter = 0;
        
        //making sure the while loop continues for 10 seconds
        while (counter < sampleDuration)
        {
            //choosing 2 random frequencies from the list
            chosenFrequencyB = (Random.Range(0, notesFrequency.Count));
            chosenFrequencyM = (Random.Range(0, notesFrequency.Count));

            //storing those chosen frequencies
            baseFrequency = notesFrequency[chosenFrequencyB];
            mainFrequency = notesFrequency[chosenFrequencyM];

            counter++;

            //making sure that both frequencies chosen at random aren't the same and if they are new frequencies are chosen
            if (chosenFrequencyB == chosenFrequencyM)
            {
                chosenFrequencyB = (Random.Range(0, notesFrequency.Count));
                chosenFrequencyM = (Random.Range(0, notesFrequency.Count));
            }


        }
        //playing the audio and saving it as a wav file
        Melody.clip = playAudio();
        Melody.Play();
        saveAsWav();



    }

    //for using 2 channeled speakers
    public void OnAudioFilterRead(float[] data, int channels)
    {
        volume = gain;
        frequency = baseFrequency * 2.0f * Mathf.PI / mainFrequency;

        for (int i = 0; i < data.Length; i = i + channels)
        {
            add += frequency;
            data[i] = (float)(volume * Mathf.Sin((float)add));

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
            else if (add > (Mathf.PI * 2))
            {
                add = 0.0;
            }
        }
    }

    //playing the audio
    public AudioClip playAudio()
    {
        //declaring variables to use later
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDuration;
        float maxValue = 1f / 4f;

        //creating the tone
        outAudioClip = AudioClip.Create("tone", sampleLength, 2, sampleRate, false);

        //storing data for the tone
        float[] samples = new float[sampleLength];
        for(int k = 0; k < sampleLength; k++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * baseFrequency * ((float)k / (float)sampleRate));
            float v = s * maxValue;
            samples[k] = v;
        }

        outAudioClip.SetData(samples, 0);
        return outAudioClip;


    }

    //saving the melody as a wav file
    public void saveAsWav()
    {
        SaveWavUtil.Save("RandomMelody", outAudioClip);
    }



}
