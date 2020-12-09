using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    private static readonly string volumePref = "volumePref";
    private float volume;
    public AudioSource audioSrc;
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        volume = PlayerPrefs.GetFloat(volumePref);
        audioSrc.volume = volume;
    }
}
