using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorActions : MonoBehaviour
{
    public bool is_closed;
    private Renderer doorRenderer;
    private Collider doorCollider;
    public AudioClip open;
    public AudioClip close;
    private AudioSource audioSource;
    private float volume = 0.4f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        doorRenderer = GetComponent<MeshRenderer>();
        doorCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Open()
    {
        is_closed = false;
        doorRenderer.enabled = false;
        doorCollider.enabled = false;
        audioSource.PlayOneShot(open, volume);
    }

    public virtual void Close()
    {
        is_closed = true;
        doorRenderer.enabled = true;
        doorCollider.enabled = true;
        audioSource.PlayOneShot(close, volume);
    }

    public virtual void Toggle()
    {
        is_closed = !is_closed;
        if (is_closed)
        {
            audioSource.PlayOneShot(close, volume);
        }
        else
        {
            audioSource.PlayOneShot(open, volume);
        }
        doorRenderer.enabled = is_closed;
        doorCollider.enabled = is_closed;
    }
}
