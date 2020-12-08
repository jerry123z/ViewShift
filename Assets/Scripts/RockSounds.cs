using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSounds : MonoBehaviour
{

    private GameObject cam;
    public AudioClip slide;
    public bool playing;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        playing = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.GetComponent<Camera_Controller>().isRotating &&
        GetComponent<RelativeRotatorData>().willRotate && !playing)
        {
            playing = true;
            audioSource.PlayOneShot(slide, 0.5f);
        }
        if (!cam.GetComponent<Camera_Controller>().isRotating)
        {
            playing = false;
        }
    }
}
