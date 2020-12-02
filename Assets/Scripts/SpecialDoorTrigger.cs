using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SpecialDoorTrigger : DoorPressurePlate
{
    public override void Start()
    {
        // Start();
        print(item);
        script = item.GetComponent<RespawnBlock>();
        print(script);
        count = 0;
        audioSource = GetComponent<AudioSource>();
    }
}
