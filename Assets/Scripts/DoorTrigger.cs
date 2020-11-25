﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    private int count;
    public GameObject item;
    private DoorActions script;

    public AudioClip push;
    private AudioSource audioSource;

    private void Start()
    {
        script = item.GetComponent<DoorActions>();
        count = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        //EnterAction?.Invoke();
        audioSource.PlayOneShot(push, 0.7f);
        print("enter");
        count += 1;
        script.Open();
    }

    void OnTriggerExit(Collider other)
    {
        print("exit");
        //ExitAction?.Invoke();
        if (count > 0)
        {
            count -= 1;
            if (count == 0){
                script.Close();
            }
        }
    }
}
