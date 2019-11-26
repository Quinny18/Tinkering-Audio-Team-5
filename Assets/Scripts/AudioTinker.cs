using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioTinker : MonoBehaviour {
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        //Creates a tone with the frequency of 20000, then plays it
        StartCoroutine(DelaySound());
    }
    

    public void StopAudio() {
        audioSource.Stop();
    }
    
   
    private AudioClip CreateTone(int frequency, int seconds, float sampleModifier) {
        //How long the sound that it creates will play for
        int sampleDurationSecs = seconds;

        //Sample rate is constant
        int sampleRate = (int) (44100 * sampleModifier);

        float maxValue = 1f / 4f;
        //SampleLength is the total number of samples in the audioclip
        int sampleLength = sampleRate * (int)sampleDurationSecs;
        //This creates an audioclip that will be edited in this function
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);
        
        //Creates an array of float at the length of sampleLength
        float[] samples = new float[sampleLength];
        //For ever sample its assigning a sound
        for (var i = 0; i < sampleLength; i++) {
            
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float) i / (float) sampleRate));

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

    IEnumerator DelaySound()
    {
        PlayOutAudio(CreateTone(1975 + Random.Range(-500, 500), 1, 0.25f));
        yield return new WaitForSeconds(0.15f);
        PlayOutAudio(CreateTone(2637 + Random.Range(-500, 500), 1, 0.25f));
        yield return new WaitForSeconds(0.25f);
        StopAudio();
    }

}