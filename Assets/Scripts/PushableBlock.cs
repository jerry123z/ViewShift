using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider coll1;
    CapsuleCollider coll2;
    GameObject player;
    bool held;
    Vector3 normalScale;

    Vector3 starting;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        starting = transform.position;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;

        coll1 = GetComponent<BoxCollider>();
        coll2 = GetComponent<CapsuleCollider>();
        player = GameObject.Find("Player");
        normalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - starting).sqrMagnitude > 1000)
        {
            transform.position = starting + Vector3.up;
        }

        if (Input.GetAxis("Hold") <= 0f && held)
        {
            coll1.enabled = true;
            coll2.enabled = true;
            rb.useGravity = true;
            held = false;
            //transform.parent = GameObject.Find("RelativeRotators").transform;
            transform.localScale = normalScale;
            transform.position = transform.position + player.transform.rotation*(new Vector3(0f, 0.2f, 0.5f));
        }
        else if (held)
        {
            Vector3 offset = new Vector3(0f, 0f, 0.5f);
            offset = player.transform.rotation * offset;
            transform.position = player.transform.position + offset;
            transform.rotation = player.transform.rotation;
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
        if (Input.GetAxis("Hold") > 0f && other == GameObject.Find("Player"))
        {
            coll1.enabled = false;
            coll2.enabled = false;
            rb.useGravity = false;
            held = true;
            //transform.parent = player.transform;
            transform.localScale = normalScale * 0.75f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (Input.GetAxis("Hold") > 0f && other == GameObject.Find("Player"))
        {
            coll1.enabled = false;
            coll2.enabled = false;
            rb.useGravity = false;
            held = true;
            //transform.parent = player.transform;
            transform.localScale = normalScale * 0.75f;
        }
    }


}
