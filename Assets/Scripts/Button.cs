using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Button allows GameObject attached_object to move by Vector3 displacement, after
    // button is pressed 
    Rigidbody rb;
    Vector3 starting;
    // Start is called before the first frame update
    bool toggled;

    public GameObject affected_object;
    Vector3 target_position;
    public Vector3 displacement;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        starting = transform.position;
    
        toggled = false;

        rb.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionY;


        target_position = affected_object.transform.position + displacement;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= starting.y - 0.1f)
        {
            rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionY;
            toggled = true;



        }
        if (toggled)
        {
            print("asdf");
            print("toggled = " + toggled);
            //if ((affected_object.transform.position - target_position).sqrMagnitude > 0.01f)
            //{
                //print("triggered");
                affected_object.transform.position = Vector3.MoveTowards(affected_object.transform.position, target_position, Time.deltaTime);
            //}
        }
    }

    void action()
    {

    }
}
