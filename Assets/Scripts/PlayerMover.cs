using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    public float Speed = 1f;
    public float JumpHeight = 1f;
    public float GroundDistance = 0.2f;
    private float distToGround;

    public Camera cam;
    private BoxCollider bc;
    private Rigidbody _body;
    public Vector3 _inputs = Vector3.zero;
    public bool _isGrounded = true;

    private GameObject touching;
    public GameObject starting;
    public AudioClip jump;
    public AudioClip land;
    private AudioSource audioSource;

    void Start()
    {
        transform.position = starting.transform.position + Vector3.up * 2;
        //print("starting at " + transform.positiosn);
        _body = GetComponent<Rigidbody>();
        bc = transform.GetComponent<BoxCollider>();
        distToGround = bc.bounds.extents.y;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _isGrounded = checkBottom();
        Quaternion offset = Quaternion.Euler(0, -45, 0);
        _inputs = Vector3.zero;
        _inputs += Input.GetAxis("Horizontal") * Vector3.Cross(-1 * cam.GetComponent<Camera_Controller>().up, offset * cam.GetComponent<Camera_Controller>().orientation);
        _inputs += Input.GetAxis("Vertical") * -1 * (offset* cam.GetComponent<Camera_Controller>().orientation);
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            audioSource.PlayOneShot(jump, 0.5f);
            _body.AddForce(5f * cam.GetComponent<Camera_Controller>().up * Mathf.Sqrt(JumpHeight), ForceMode.VelocityChange);
            touching = null;
        }

        if ((transform.position - starting.transform.position).sqrMagnitude > 1000)
        {
            transform.position = starting.transform.position + Vector3.up * 2;
            cam.GetComponent<Camera_Controller>().up = Vector3.up;
            Physics.gravity = -1 * Vector3.up * 9.8f;
            _body.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        touching = collision.gameObject;
    }

    public void snap()
    {
        if (touching)
        {
            transform.position = touching.transform.position + Vector3.up;
        }
    }

    bool checkBottom()
    {
        return Physics.Raycast(transform.position, -cam.GetComponent<Camera_Controller>().up, (float)(distToGround + 0.1)) ||
        Physics.Raycast(transform.position + new Vector3(0, 0, 0.5f), -cam.GetComponent<Camera_Controller>().up, (float)(distToGround + 0.1)) ||
        Physics.Raycast(transform.position + new Vector3(0, 0, -0.5f), -cam.GetComponent<Camera_Controller>().up, (float)(distToGround + 0.1)) ||
        Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), -cam.GetComponent<Camera_Controller>().up, (float)(distToGround + 0.1)) ||
        Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), -cam.GetComponent<Camera_Controller>().up, (float)(distToGround + 0.1));
    }

    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}