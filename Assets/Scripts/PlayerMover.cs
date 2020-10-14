using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

    public float Speed = 1f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;
    private float distToGround;

    public Camera cam;
    private BoxCollider bc;
    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    public GameObject touching;

    public GameObject starting;

    void Start()
    {
        transform.position = starting.transform.position + Vector3.up * 2;
        //print("starting at " + transform.positiosn);
        _body = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<Transform>();
        bc = transform.GetComponent<BoxCollider>();
        distToGround = bc.bounds.extents.y;
    }

    void Update()
    {
        //transform.position = Vector3.zero;
        _isGrounded = checkBottom();

        _inputs = Vector3.zero;
        _inputs += Input.GetAxis("Horizontal") * Vector3.Cross(-1 * cam.GetComponent<Camera_Controller>().up, cam.GetComponent<Camera_Controller>().orientation);
        //_inputs.x = Input.GetAxis("Horizontal");
        //_inputs.z = Input.GetAxis("Vertical");
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {

            //print("checking up is " + cam.GetComponent<Camera_Controller>().up);
            //_body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
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
        //print(collision.gameObject);

        touching = collision.gameObject;
    }

    public void snap()
    {
        //print("snapping");
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
        //return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround+0.1))||
        //Physics.Raycast(transform.position + new Vector3(0,0,0.5f), -Vector3.up, (float)(distToGround+0.1))||
        //Physics.Raycast(transform.position + new Vector3(0,0,-0.5f), -Vector3.up, (float)(distToGround+0.1))||
        //Physics.Raycast(transform.position + new Vector3(0.5f,0,0), -Vector3.up, (float)(distToGround+0.1))||
        //Physics.Raycast(transform.position + new Vector3(-0.5f,0,0), -Vector3.up, (float)(distToGround+0.1));
        // This checks from your centre, and 0.5 in both x and z directions away from the centre.
    }

    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}