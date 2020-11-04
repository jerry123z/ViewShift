using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialButton : MonoBehaviour
{
    // Button allows GameObject attached_object to move by Vector3 displacement, after
    // button is pressed 
    Rigidbody rb;
    Vector3 starting;
    // Start is called before the first frame update
    public bool toggled;
    public bool restoring;

    public GameObject affected_object;
    Vector3 target_position;
    Vector3 target_starting;
    public Vector3 displacement;

    bool rebooted;

    int collision_count = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        starting = transform.position;
    
        toggled = false;

        rb.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionY;

        target_starting = affected_object.transform.position;

        target_position = affected_object.transform.position + displacement;

        restoring = true;

        rebooted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= starting.y - 0.1f)
        {
            rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionY;
            toggled = true;
        } else if (Mathf.Abs(transform.position.y - starting.y) <= 0.1f)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionY;
            toggled = false;
            rebooted = false;
        }
        if (toggled)
        {
            if (!rebooted)
            {
                reboot_affected();
                rebooted = true;
            }
        }
        if (restoring)
        {
            transform.position = Vector3.MoveTowards(transform.position, starting, Time.deltaTime);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        restoring = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        restoring = true;
    }


    private void reboot_affected()
    {
        affected_object.transform.position = target_starting;
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    collision_count++;
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    collision_count--;
    //    //transform.position = Vector3.MoveTowards(transform.position, starting, Time.deltaTime);
    //}
}
