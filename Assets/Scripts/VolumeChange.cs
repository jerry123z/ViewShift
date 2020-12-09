using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    private static readonly string Firstplay = "Firstplay";
    private static readonly string volumePref = "volumePref";
    private int firstPlayInt;
    public Slider volumeSlider;
    private float volume;
    private AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        firstPlayInt = PlayerPrefs.GetInt(Firstplay);
        print(firstPlayInt);

        if(firstPlayInt == 0)
        {
            volume = .5f;
            volumeSlider.value = volume;
            PlayerPrefs.SetFloat(volumePref, volume);
            PlayerPrefs.SetInt(Firstplay, -1);
        }
        else
        {
            volume = PlayerPrefs.GetFloat(volumePref);
            print(volume);
            volumeSlider.value = volume; 
            audioSrc.volume = volumeSlider.value;
        }
    }

    public void saveSound()
    {
        PlayerPrefs.SetFloat(volumePref, volumeSlider.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            saveSound();
        }
    }

    public void UpdateSound()
    {
        audioSrc.volume = volumeSlider.value;
    }
}
