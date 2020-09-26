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

    public float speed = 6f;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        sc = transform.GetComponent<SphereCollider>();
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
    }

    bool checkBottom()
    {
        return Physics.Raycast(sc.bounds.center, Vector3.forward);
    }
}
