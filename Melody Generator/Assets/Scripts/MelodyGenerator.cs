using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MelodyGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource Melody;
    private AudioClip outAudioClip;

    private double frequency;

    private int sampleLength = 10;

    private double baseFrequency;
    private double mainFrequency;

    private double add;

    private float volume = 0.2f;

 
    

    void Start()
    {
        Melody = GetComponent<AudioSource>();
        
        playAudio();
        //SaveWavUtil.Save("C://Users//Ceri Thomas//Documents//Tinkering-Audio-Team-5//Melody Generator//soundFile.wav", outAudioClip);

    }


    public void MakeMelody()
    {
        var notesFrequency = new List<double> {
            261.63, //C4
            293.66, //D4
            329.63, // E4
            349.23, //F4
            392.00, //G4
            440.00, //A4
            493.88, //B4
            277.18, //C#4
            311.13, //D#4
            369.99, //F#4
            415.30, //G#4
            466.16, //A#4
            16.35, //C0
            18.35, //D0
            20.60, //E0
            21.83, //F0
            24.50, //G0
            27.50, //A0
            30.87, //B0
            17.32, //C#0
            19.45, //D#0
            23.12, //F#0
            25.96, //G#0
            29.14, //A#0
            32.70, //C1
            36.71, //D1
            41.20, //E1
            43.65, //F1
            49.00, //G1
            55.00, //A1
            61.74, //B1
            34.65, //C#1
            38.89, //D#1
            46.25, //F#1
            51.91, //G#1
            58.27, //A#1
            65.41, //C2
            73.42, //D2
            82.41, //E2
            87.32, //F2
            98.00, //G2
            110.00, //A2
            123.47, //B2
            69.30, //C#2
            77.78, //D#2
            92.50, //F#2 
            103.83, //G#2
            116.54, //A#2
            130.81, //C3
            146.83, //D3
            164.81, //E3
            174.61, //F3
            196.00, //G3
            220.00, //A3
            246.94, //B3
            138.59, //C#3
            155.56, //D#3
            185.00, //F#3
            207.65, //G#3
            233.08, //A#3
            523.25, //C5
            587.33, //D5
            659.25, //E5
            698.46, //F5
            783.99, //G5
            880.00, //A5
            987.77, //B5 
            554.37, //C#5
            622.25, //D#5
            739.99, //F#5
            830.61, //G#5
            932.33, //A#5
            1046.50, //C6
            1174.66, //D6
            1318.51, //E6 
            1396.91, //F6
            1567.98, //G6
            1760.00, //A6
            1975.53, //B6
            1108.73, //C#6
            1244.51, //D#6
            1479.98, //F#6
            1567.98, //G#6
            1864.66, //A#6
            2093.00, //C7
            2349.32, //D7
            2637.02, //E7
            2793.83, //F7
            3135.96, //G7
            3530.00, //A7
            3951.07, //B7
            2217.46, //C#7
            2489.02, //D#7
            2959.96, //F#7
            3322.44, //G#7
            3729.31, //A#7
            4186.01, //C8
            4698.63, //D8
            5274.04, //E8
            5587.65, //F8
            6271.93, //G8
            7040.00, //A8
            7902.13, //B8
            4434.92, //C#8
            4978.03, //D#8
            5919.91, //F#8
            6644.88, //G#8
            1458.62, //A#8
        };

        //make the base for the melody
        //add two or more waves together
        // var random = new System.Random();
        // for (int i = 0; i < 109; i++)
        // {
        //    outAudioClip = CreateTone(random.Next(notesFrequency.Count));

        // }

        var counter = 0;
        while (counter < sampleLength)
        {
            frequency = 261.63f;
            baseFrequency = Random.Range(0, 109);
            mainFrequency = Random.Range(0, 109);

            counter++;

            if (baseFrequency == mainFrequency)
            {
                baseFrequency = Random.Range(0, 109);
                mainFrequency = Random.Range(0, 109);
            }

            
        }

        playAudio();
        Melody.clip = outAudioClip;
        Melody.Play();
        saveAsWav();



    }

    public void OnAudioFilterRead(float[] data, int channels)
    {
        frequency = baseFrequency * 2.0 * Mathf.PI / mainFrequency;

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

    public void playAudio()
    {
        outAudioClip = AudioClip.Create("tone", (int)(frequency * sampleLength), 2, (int)frequency, false);
    }

    public void saveAsWav()
    {
        SaveWavUtil.Save("RandomMelody", outAudioClip);
    }



}
