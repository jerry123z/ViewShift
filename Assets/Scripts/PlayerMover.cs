﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

    public float Speed = 1f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;
    private float distToGround;

    public Camera cam;
    private SphereCollider sc;
    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    public GameObject touching; 

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<Transform>();
        sc = transform.GetComponent<SphereCollider>();
        distToGround = sc.bounds.extents.y;
    }

    void Update()
    {
        _isGrounded = checkBottom();


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
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround+0.1));
    }

    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}