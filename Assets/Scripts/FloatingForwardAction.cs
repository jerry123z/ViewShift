using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatingForwardAction : MonoBehaviour
{
    public bool is_floating;
    private Renderer doorRenderer;
    private Collider doorCollider;
    public AudioClip open;
    public AudioClip close;
    private AudioSource audioSource;
    private float volume = 0.4f;

    public GameObject floatingPad;
    public GameObject ramp;
    public GameObject cube;

    enum floatingStates { moving, pause }
    floatingStates states;

    public float smooth;

    Vector3 newPos;

    int count;

    int direction;

    // Start is called before the first frame update
    public virtual void Start()
    {
        count = 0;
        doorRenderer = GetComponent<MeshRenderer>();
        doorCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        states = floatingStates.pause;
    }

    public virtual void Update()
    {
        FSM();
    }

    public virtual void FloatingForward()
    {
        is_floating = true;
        states = floatingStates.moving;
        direction = 1;
        doorRenderer.enabled = false;
        doorCollider.enabled = false;
        audioSource.PlayOneShot(open, volume);
    }

    public virtual void FloatingLeft()
    {
        is_floating = false;
        states = floatingStates.moving;
        direction = 2;
        doorRenderer.enabled = true;
        doorCollider.enabled = true;
        audioSource.PlayOneShot(close, volume);
    }

    public virtual void FloatingRight()
    {
        is_floating = false;
        states = floatingStates.moving;
        direction = 3;
        doorRenderer.enabled = true;
        doorCollider.enabled = true;
        audioSource.PlayOneShot(close, volume);
    }

    public void FSM()
    {
        if (states == floatingStates.moving)
        {
            ramp.transform.parent = floatingPad.transform;
            cube.transform.parent = floatingPad.transform;
        }
    }
}
