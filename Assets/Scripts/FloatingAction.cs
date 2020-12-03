using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatingAction : MonoBehaviour
{
    public bool is_floating;
    private Renderer doorRenderer;
    private Collider doorCollider;
    public AudioClip open;
    public AudioClip close;
    private AudioSource audioSource;
    private float volume = 0.4f;

    public GameObject floatingPad;

    enum floatingStates {goUp, goDown, bottom, PauseState}
    floatingStates states;

    public Transform top;
    public Transform bottom;

    public float smooth;

    Vector3 newPos;

    int count;

    // Start is called before the first frame update
    public virtual void Start()
    {
        count = 0;
        doorRenderer = GetComponent<MeshRenderer>();
        doorCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        states = floatingStates.PauseState;
    }

    public virtual void Update()
    {
        FSM();
    }

    public virtual void Floating()
    {
        is_floating = true;
        states = floatingStates.goUp;
        doorRenderer.enabled = false;
        doorCollider.enabled = false;
        audioSource.PlayOneShot(open, volume);
    }

    public virtual void Stationary()
    {
        is_floating = false;
        states = floatingStates.bottom;
        doorRenderer.enabled = true;
        doorCollider.enabled = true;
        audioSource.PlayOneShot(close, volume);
    }

    public virtual void Toggle()
    {
        is_floating = !is_floating;
        if (is_floating)
        {
            audioSource.PlayOneShot(open, volume);
            Floating();
        }
        else
        {
            audioSource.PlayOneShot(close, volume);
            Stationary();
        }
    }

    public void FSM()
    {
        if(states == floatingStates.goUp)
        {
            newPos = top.position;
            transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
            if(transform.position == top.position)
            {
                states = floatingStates.goDown;
            }
        }
        if (states == floatingStates.goDown)
        {
            newPos = bottom.position;
            transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
            if (transform.position == bottom.position)
            {
                states = floatingStates.bottom;
            }
        }
        if (states == floatingStates.bottom)
        {
            transform.position = bottom.position;
        }
    }
}
