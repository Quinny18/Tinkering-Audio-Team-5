using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyGeneratortest : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource Melody;
    bool m_Play;
    bool m_ToggleChange;
    float frequency = 16.35f;
    float wavelength = 2109.89f;
    int sampleLength = 3;

    public void Start()
    {
        Melody = GetComponent<AudioSource>();
        m_Play = true;
        Debug.Log("Started");
    }

    // Update is called once per frame
    public void Update()
    {
        if (m_Play == true && m_ToggleChange == true)
        {
            Melody.Play();
            m_ToggleChange = false;
        }

        if (m_Play == false && m_ToggleChange == true)
        {
            Melody.Stop();
            m_ToggleChange = false;
            Debug.Log("Pause");
        }
    }
}
