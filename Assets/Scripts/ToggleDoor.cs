using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoor : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }
    void Open()
    {
        is_closed = false;
        doorRenderer.enabled = false;
        doorCollider.enabled = false;
    }

    void Close()
    {
        is_closed = true;
        doorRenderer.enabled = true;
        doorCollider.enabled = true;
    }

    void Toggle()
    {
        is_closed = !is_closed;
        doorRenderer.enabled = is_closed;
        doorCollider.enabled = is_closed;
    }
}
