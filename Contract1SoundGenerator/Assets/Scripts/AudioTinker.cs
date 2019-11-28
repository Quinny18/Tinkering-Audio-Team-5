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
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    private SFB_AudioClipArrayCombiner combiner;
    public int Frequency1;
    public int Frequency2;
    public int Frequency3;

    public int Delay1;
    public int Delay2;
    public int Delay3;

    //Frequency Sliders
    public Slider Slider1;
    public Slider Slider2;
    public Slider Slider3;
    //Delay Sliders
    public Slider Slider4;
    public Slider Slider5;
    public Slider Slider6;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        combiner = GetComponent<SFB_AudioClipArrayCombiner>();
    }

    void Update()
    {
        Frequency1 = (int)Slider1.value;
        Frequency2 = (int)Slider2.value;
        Frequency3 = (int)Slider3.value;

        Delay1 = (int)Slider4.value;
        Delay2 = (int)Slider5.value;
        Delay3 = (int)Slider6.value;
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    private AudioClip CreatePickupSound(int[] frequency, int[] seconds, float[] sampleModifier)
    {
        
        //Sample rate is constant
        int sampleRate = 44100;

        float maxValue = 1f / 4f;

        //SampleLength is the total number of samples in the audioclip
        //
        int sampleLength1 = (int)(sampleRate * sampleModifier[0]) * seconds[0];
        int sampleLength2 = (int)(sampleRate * sampleModifier[1]) * seconds[1];
        int sampleLength3 = (int)(sampleRate * sampleModifier[2]) * seconds[2];
        int sampleLength = sampleLength1 + sampleLength2 + sampleLength3;

        //This creates an audioclip that will be edited in this function
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        //Creates an array of float at the length of sampleLength
        float[] samples = new float[sampleLength];
        //For ever sample its assigning a sound
        
        for (var i = 0; i < sampleLength; i++)
        {
            float s = 0;
            if (i > sampleLength1)
            {
                s = Mathf.Sin(2.0f * Mathf.PI * frequency[1] * ((float)i / (float)sampleRate));
            }
            else if(i > sampleLength2)
            {
                s = Mathf.Sin(2.0f * Mathf.PI * frequency[2] * ((float)i / (float)sampleRate));
            }

            else
            {
                s = Mathf.Sin(2.0f * Mathf.PI * frequency[0] * ((float)i / (float)sampleRate));
            }

            float v = s * maxValue;

            samples[i] = v;
        }
        //Assigning the audio to the audioclip
        audioClip.SetData(samples, 0);
        return audioClip;
    }
    

    //This outputs the sound
    public void PlayOutAudio(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
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
        SaveWavUtil.Save(path, outAudioClip);
    }

    //This function is creating a list of audio clips and adding the audioClip to that list
    public void SoundClips(AudioClip audioClip)
    {
        List<AudioClip> Clips = new List<AudioClip>();
        Clips.Add(audioClip);
    }
    

    IEnumerator DelaySound()
    {
        int[] Frequencies = new int[3] {Frequency1, Frequency2, Frequency3};
        int[] Seconds = new int[3] {Delay1, Delay2, Delay3};
        float[] Modifier = new float[3] { 0.25f, 0.15f, 0.25f };
        outAudioClip = CreatePickupSound(Frequencies, Seconds, Modifier);
        PlayOutAudio(outAudioClip);
        yield return new WaitForSeconds(0);
    }

   

    /*IEnumerator DelaySound()
    {
        AudioClip audioClip = CreateTone(1975, 1, 0.25f);
        PlayOutAudio(audioClip);
        combiner.audioLayers[0].clip[0] = audioClip;
        yield return new WaitForSeconds(0.15f);

        audioClip = CreateTone(2637, 1, 0.25f);
        PlayOutAudio(audioClip);
        combiner.audioLayers[1].clip[0] = audioClip;

        yield return new WaitForSeconds(0.25f);
        StopAudio();

        audioClip.
    }
    */
    //1975, 1, 0.25f
    //2637, 1, 0.25f
}





























