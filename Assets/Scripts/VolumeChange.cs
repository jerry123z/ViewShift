using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChange : MonoBehaviour
{
    private AudioSource audioSrc;
    private float volume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = volume;
    }

    public void setVolume(float vol)
    {
        volume = vol;
    }
}
