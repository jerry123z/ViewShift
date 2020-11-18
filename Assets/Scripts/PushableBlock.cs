using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    Rigidbody rb;

    Vector3 starting;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        starting = transform.position;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - starting).sqrMagnitude > 1000)
        {
            transform.position = starting + Vector3.up;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.layer == 10)
        {
            double distx = Mathf.Abs(other.transform.position.x - transform.position.x);
            double distz = Mathf.Abs(other.transform.position.z - transform.position.z);

            //if (distx <= distz)
            //{
            //    rb.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionZ;
            //} else if (distz <= distx)
            //{
            //    rb.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionX;
            //}
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }


}
