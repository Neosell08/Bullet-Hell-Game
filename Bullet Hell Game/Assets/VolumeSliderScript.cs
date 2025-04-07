using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliderScript : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider volumeSlider;

    void Start()
    {
        // Load saved volume
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume");
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);
        }
        else
        {
            volumeSlider.value = 0.75f; // Default volume
        }
    }

    public void SetVolume(float volume)
    {

        // Convert linear 0-1 to logarithmic dB scale (-80 to 0)
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        if (volume == 0)
        {
            masterMixer.SetFloat("MasterVolume", -999999f);
            Debug.Log("aa");
        }
        Debug.Log(volume);

        // Save setting
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
