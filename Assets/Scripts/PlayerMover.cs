﻿using System.Collections;
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

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<Transform>();
        bc = transform.GetComponent<BoxCollider>();
        distToGround = bc.bounds.extents.y;
    }

    void Update()
    {
        _isGrounded = checkBottom();
        bc.size = new Vector3(0.5f,1f,0.5f);

        _inputs = Vector3.zero;
        _inputs += Input.GetAxis("Horizontal") * Vector3.Cross(Vector3.down, cam.GetComponent<Camera_Controller>().orientation);
        //_inputs.x = Input.GetAxis("Horizontal");
        //_inputs.z = Input.GetAxis("Vertical");
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            touching = null;
        }

        if (transform.position.y < -5)
        {
            transform.position = new Vector3(3, 1, -1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject);

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
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround+0.1))||
        Physics.Raycast(transform.position + new Vector3(0,0,0.5f), -Vector3.up, (float)(distToGround+0.1))||
        Physics.Raycast(transform.position + new Vector3(0,0,-0.5f), -Vector3.up, (float)(distToGround+0.1))||
        Physics.Raycast(transform.position + new Vector3(0.5f,0,0), -Vector3.up, (float)(distToGround+0.1))||
        Physics.Raycast(transform.position + new Vector3(-0.5f,0,0), -Vector3.up, (float)(distToGround+0.1));
        // This checks from your centre, and 0.5 in both x and z directions away from the centre.
    }

    void FixedUpdate()
    {
        if (_groundChecker.parent == null)
        {
            _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
        }
        else
        {
            _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
        }
    }
}