using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnBlock : DoorActions
{
    // public bool is_closed;
    // publc` Renderer doorRenderer;
    // private Collider doorCollider;

    Vector3 start_position;
    // Start is called before the first frame update
    public override void Start()
    {   
        start_position = transform.position;
    }

    public override void Open()
    {
        transform.position = start_position;
    }

    public override void Close()
    {
    }

    public override void Toggle()
    {
    }
}
