using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SpecialDoorTrigger : DoorTrigger
{
    public override void Start()
    {
        // Start();
        script = item.GetComponent<RespawnBlock>();
        count = 0;
        audioSource = GetComponent<AudioSource>();
    }
}
