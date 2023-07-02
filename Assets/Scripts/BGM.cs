using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BGM : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider audioSlider;
    public void AudioControl()
    {
        float sound = audioSlider.value;

        if(sound == -40f)
            audioMixer.SetFloat("BGM", -80);
        else
            audioMixer.SetFloat("BGM", sound);
    }
}
