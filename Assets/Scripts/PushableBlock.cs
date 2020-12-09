using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider coll1;
    CapsuleCollider coll2;
    public GameObject player;
    bool held;
    Vector3 normalScale;
    GameObject instruction;

    Vector3 starting;
    // Start is called before the first frame update
    void Start()
    {
        instruction = GameObject.Find("PickupInstruction");
        if(instruction.activeSelf)
        {
            instruction.SetActive(false);
        }
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
        if (Vector3.Distance(player.transform.position, transform.position) < 1.5)
        {
            if (!instruction.activeSelf && !held)
            {
                instruction.SetActive(true);
            }
            if(held && instruction.activeSelf)
            {
                instruction.SetActive(false);
            }
        }
        else
        {
            if (instruction.activeSelf)
            {
                instruction.SetActive(false);
            }
        }
        if ((transform.position.y - starting.y) < - 200)
        {
            transform.position = starting + Vector3.up;
        }

        if (Input.GetAxis("Hold") <= 0f && held)
        {
            coll1.enabled = true;
            coll2.enabled = true;
            rb.useGravity = true;
            held = false;
            transform.localScale = normalScale;
            transform.position = transform.position + player.transform.rotation*(new Vector3(0f, 0.1f, 0.2f));
        }
        else if (held)
        {
            Vector3 offset = new Vector3(0f, 0.5f, 0.7f);
            offset = player.transform.rotation * offset;
            transform.position = player.transform.position + offset;
            transform.rotation = player.transform.rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (Input.GetAxis("Hold") > 0f && other == GameObject.Find("Player"))
        {
            coll1.enabled = false;
            coll2.enabled = false;
            rb.useGravity = false;
            held = true;
            //transform.parent = player.transform;
            //transform.localScale = normalScale * 0.75f;
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
            //transform.localScale = normalScale * 0.75f;
        }
    }


}
