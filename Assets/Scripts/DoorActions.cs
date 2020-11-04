using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorActions : MonoBehaviour
{
    public bool is_closed;
    private Renderer doorRenderer;
    private Collider doorCollider;

    // Start is called before the first frame update
    void Start()
    {
        doorRenderer = GetComponent<MeshRenderer>();
        doorCollider = GetComponent<BoxCollider>();
    }

    public void Open()
    {
        is_closed = false;
        doorRenderer.enabled = false;
        doorCollider.enabled = false;
    }

    public void Close()
    {
        is_closed = true;
        doorRenderer.enabled = true;
        doorCollider.enabled = true;
    }

    public void Toggle()
    {
        is_closed = !is_closed;
        doorRenderer.enabled = is_closed;
        doorCollider.enabled = is_closed;
    }
}
