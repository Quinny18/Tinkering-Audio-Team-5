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
        var notesFrequency = new Dictionary<string, float> {
            ["C4"] = 261.63f,
            ["D4"] = 293.66f,
            ["E4"] = 329.63f,
            ["F4"] = 349.23f,
            ["G4"] = 392.00f,
            ["A4"] = 440.00f,
            ["B4"] = 493.88f,
            ["C#4"] = 277.18f,
            ["D#4"] = 311.13f,
            ["F#4"] = 369.99f,
            ["G#4"] = 415.30f,
            ["A#4"] = 466.16f,
            ["C0"] = 16.35f,
            ["D0"] = 18.35f,
            ["E0"] = 20.60f,
            ["F0"] = 21.83f,
            ["G0"] = 24.50f,
            ["A0"] = 27.50f,
            ["B0"] = 30.87f,
            ["C#0"] = 17.32f,
            ["D#0"] = 19.45f,
            ["F#0"] = 23.12f,
            ["G#0"] = 25.96f,
            ["A#0"] = 29.14f,
            ["C1"] = 32.70f,
            ["D1"] = 36.71f,
            ["E1"] = 41.20f,
            ["F1"] = 43.65f,
            ["G1"] = 49.00f,
            ["A1"] = 55.00f,
            ["B1"] = 61.74f,
            ["C#1"] = 34.65f,
            ["D#1"] = 38.89f,
            ["F#1"] = 46.25f,
            ["G#1"] = 51.91f,
            ["A#1"] = 58.27f,
            ["C2"] = 65.41f,
            ["D2"] = 73.42f,
            ["E2"] = 82.41f,
            ["F2"] = 87.32f,
            ["G2"] = 98.00f,
            ["A2"] = 110.00f,
            ["B2"] = 123.47f,
            ["C#2"] = 69.30f,
            ["D#2"] = 77.78f,
            ["F#2"] = 92.50f,
            ["G#2"] = 92.50f,
            ["A#2"] = 116.54f,
            ["C3"] = 130.81f,
            ["D3"] = 146.83f,
            ["E3"] = 164.81f,
            ["F3"] = 174.61f,
            ["G3"] = 196.00f,
            ["A3"] = 220.00f,
            ["B3"] = 246.94f,
            ["C#3"] = 138.59f,
            ["D#3"] = 155.56f,
            ["F#3"] = 185.00f,
            ["G#3"] = 207.65f,
            ["A#3"] = 233.08f,
            ["C5"] = 523.25f,
            ["D5"] = 587.33f,
            ["E5"] = 659.25f,
            ["F5"] = 698.46f,
            ["G5"] = 783.99f,
            ["A5"] = 880.00f,
            ["B5"] = 987.77f,
            ["C#5"] = 554.37f,
            ["D#5"] = 622.25f,
            ["F#5"] = 739.99f,
            ["G#5"] = 830.61f,
            ["A#5"] = 932.33f,
            ["C6"] = 1046.50f,
            ["D6"] = 1174.66f,
            ["E6"] = 1396.91f,
            ["F6"] = 1396.91f,
            ["G6"] = 1567.98f,
            ["A6"] = 1760.00f,
            ["B6"] = 1975.53f,
            ["C#6"] = 1108.73f,
            ["D#6"] = 1244.51f,
            ["F#6"] = 1479.98f,
            ["G#6"] = 1567.98f,
            ["A#6"] = 1864.66f,
            ["C7"] = 2093.00f,
            ["D7"] = 2349.32f,
            ["E7"] = 2637.02f,
            ["F7"] = 2793.83f,
            ["G7"] = 3135.96f,
            ["A7"] = 3530.00f,
            ["B7"] = 3951.07f,
            ["C#7"] = 2217.46f,
            ["D#7"] = 2489.02f,
            ["F#7"] = 2959.96f,
            ["G#7"] = 3322.44f,
            ["A#7"] = 3729.31f,
            ["C8"] = 4186.01f,
            ["D8"] = 4698.63f,
            ["E8"] = 5274.04f,
            ["F8"] = 5587.65f,
            ["G8"] = 6271.93f,
            ["A8"] = 7040.00f,
            ["B8"] = 7902.13f,
            ["C#8"] = 4434.92f,
            ["D#8"] = 4978.03f,
            ["F#8"] = 5919.91f,
            ["G#8"] = 6644.88f,
            ["A#8"] = 1458.62f,
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
