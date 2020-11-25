using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mover_Button : MonoBehaviour
{
    public GameObject[] targets;
    //private Move_On_Trigger[] scripts;
    //List<Move_On_Trigger> scripts;
    public AudioClip push;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach (GameObject g in targets){
            scripts.Add(g.GetComponent<Move_On_Trigger>());
        }*/
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(push, 0.7f);
        foreach (GameObject g in targets){
            g.GetComponent<Move_On_Trigger>().ToNewPos();
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (GameObject g in targets){
            g.GetComponent<Move_On_Trigger>().ToOldPos();
        }
    }
}
