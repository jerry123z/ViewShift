using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public CharacterController controller;
    public camera cam;
    private Vector3 orientation;
    private Rigidbody rb;
    private SphereCollider sc;
    private float distToGround;

    public float speed = 6f;

    private float grav = -9.8f;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        sc = transform.GetComponent<SphereCollider>();
        distToGround = sc.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        bool temp = checkBottom();
        print(temp);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(-horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
        Vector3 vert = new Vector3(0f, grav, 0f);
        controller.Move(vert * Time.deltaTime);

    }

    bool checkBottom()
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround+0.1));
    }
}
