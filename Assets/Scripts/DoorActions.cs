using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorActions : MonoBehaviour
{
    public bool is_closed;
    public Renderer doorRenderer;
    public Collider doorCollider;

    // Start is called before the first frame update
    public virtual void Start()
    {
        doorRenderer = GetComponent<MeshRenderer>();
        doorCollider = GetComponent<BoxCollider>();
    }

    public virtual void Open()
    {
        is_closed = false;
        doorRenderer.enabled = false;
        doorCollider.enabled = false;
    }

    public virtual void Close()
    {
        is_closed = true;
        doorRenderer.enabled = true;
        doorCollider.enabled = true;
    }

    public virtual void Toggle()
    {
        is_closed = !is_closed;
        doorRenderer.enabled = is_closed;
        doorCollider.enabled = is_closed;
    }
}
