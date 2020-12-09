using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioListenerVolume : MonoBehaviour
{
    public Slider volumeSlider;
    private float volume;
    // Start is called before the first frame update
    void Start()
    {
        volume = 0.5f;
        volumeSlider.normalizedValue = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        print(volume);
        AudioListener.volume = volume * 3;
    }

    public void SetVolume(float vol)
    {
        volume = vol;
    }
}
