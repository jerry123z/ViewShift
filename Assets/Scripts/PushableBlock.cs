using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.layer == 10)
        {
            double distx = Mathf.Abs(other.transform.position.x - transform.position.x);
            double distz = Mathf.Abs(other.transform.position.z - transform.position.z);

            if (distx <= distz)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionZ;
            } else if (distz <= distx)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionX;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
    }


}
