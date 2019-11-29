/*
 * Author of this Script: Callum Quinn
 * Contributions by: Callum Quinn and Cole Gilbert
 * Link to GitHub: https://github.com/Quinny18/Tinkering-Audio-Team-5
 * License used: MIT 
 * 
 * This script includes all the necessary code for the creation of a tool
 * that generates different noises that could be used as sound effects in a 
 * game. The user is able to specific the Frequency of each audio clip, the 
 * delay between each clip playing and a modifier which changes the length
 * of the clips. Those sound clips are generated using the parameters that
 * the user inputs via the UI and then can be saved into a wav. file anywhere
 * on the computer.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioTinker : MonoBehaviour
{
    public int Sound1;
    private AudioSource @AudioSource;
    private AudioClip OutAudioClip;
    
    
    //Initialising variables for sound generation
    public int Frequency1;
    public int Frequency2;
    public int Frequency3;
    //The delay in between the clips playing
    public int Delay1;
    public int Delay2;
    public int Delay3;

    public float Modifier1;
    public float Modifier2;
    public float Modifier3;

    //Frequency Sliders
    public Slider FrequencySlider1;
    public Slider FrequencySlider2;
    public Slider FrequencySlider3;
    //Delay Sliders
    public Slider DelaySlider1;
    public Slider DelaySlider2;
    public Slider DelaySlider3;
    //Modifier Sliders
    public Slider ModifierSlider1;
    public Slider ModifierSlider2;
    public Slider ModifierSlider3;

    // Start is called before the first frame update
    void Start()
    {
        @AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Assigning the value of the Sliders to the variables
        Frequency1 = (int)FrequencySlider1.value;
        Frequency2 = (int)FrequencySlider2.value;
        Frequency3 = (int)FrequencySlider3.value;

        Delay1 = (int)DelaySlider1.value;
        Delay2 = (int)DelaySlider2.value;
        Delay3 = (int)DelaySlider3.value;

        Modifier1 = ModifierSlider1.value;
        Modifier2 = ModifierSlider2.value;
        Modifier3 = ModifierSlider3.value;

    }
    //This stops all the current audio
    public void StopAudio()
    {
       @AudioSource.Stop();
    }
    //This function creates the sound clips
    private AudioClip CreatePickupSound(int[] Frequency, int[] Seconds, float[] SampleModifier)
    {
        
        //Sample rate is constant
        int SampleRate = 44100;

        float MaxValue = 1f / 4f;

        //SampleLength is the total number of samples in the audioclip
        //
        int SampleLength1 = (int)(SampleRate * SampleModifier[0]) * Seconds[0];
        int SampleLength2 = (int)(SampleRate * SampleModifier[1]) * Seconds[1];
        int SampleLength3 = (int)(SampleRate * SampleModifier[2]) * Seconds[2];
        int SampleLength = SampleLength1 + SampleLength2 + SampleLength3;

        //This creates an audioclip that will be edited in this function
        var audioClip = AudioClip.Create("tone", SampleLength, 1, SampleRate, false);

        //Creates an array of float at the length of SampleLength
        float[] Samples = new float[SampleLength];
        //For ever sample its assigning a sound
        
        for (var i = 0; i < SampleLength; i++)
        {
            float s = 0;
            if (i > SampleLength1)
            {
                s = Mathf.Sin(2.0f * Mathf.PI * Frequency[1] * ((float)i / (float)SampleRate));
            }
            else if(i > SampleLength2)
            {
                s = Mathf.Sin(2.0f * Mathf.PI * Frequency[2] * ((float)i / (float)SampleRate));
            }

            else
            {
                s = Mathf.Sin(2.0f * Mathf.PI * Frequency[0] * ((float)i / (float)SampleRate));
            }

            float v = s * MaxValue;

            Samples[i] = v;
        }
        //Assigning the audio to the audioclip
        audioClip.SetData(Samples, 0);
        return audioClip;
    }

    //This outputs the sound
    public void PlayOutAudio(AudioClip sound)
    {
        @AudioSource.PlayOneShot(sound);
    }
    //When the Play button is pressed it runs the Coroutine
    public void PlaySoundBtn()
    {
        StartCoroutine(DelaySound());
    }

    //This creates the prompt to the user to input a save location
    public void SaveWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        SaveWavUtil.Save(path, OutAudioClip);
    }

    //This function is creating a list of audio clips and adding the audioClip to that list
    public void SoundClips(AudioClip audioClip)
    {
        List<AudioClip> Clips = new List<AudioClip>();
        Clips.Add(audioClip);
    }
    //This Coroutine adds all of the variables into arrays to then be outputted
    IEnumerator DelaySound()
    {
        int[] Frequencies = new int[3] {Frequency1, Frequency2, Frequency3};
        int[] Seconds = new int[3] {Delay1, Delay2, Delay3};
        float[] Modifier = new float[3] {Modifier1, Modifier2, Modifier3};
        OutAudioClip = CreatePickupSound(Frequencies, Seconds, Modifier);
        PlayOutAudio(OutAudioClip);
        yield return new WaitForSeconds(0);
    }
}





























