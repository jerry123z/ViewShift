using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 center;
    public static Vector3 orientation;
    public static float scale;
    public static float speed;
    public static Camera c;
    void Start()
    {
        center = new Vector3(1, 3, -5) ;
        scale = 10f;
        speed = 1f;
        orientation = scale * Vector3.forward;
        transform.position = center + orientation;
        print(orientation.x);
        print(orientation.y);
        print(orientation.z);
        transform.LookAt(center);
        c = GetComponent<Camera>();
    }

    void rotate(Vector3 direction)
    {
        orientation = scale * direction;
        //transform.position = center + orientation;
        //transform.LookAt(center);
    }


    // Update is called once per frame
    void Update()
    {

        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, center + orientation, step);
        transform.LookAt(center);

        if (Input.inputString == "a")
        {
            rotate(Vector3.right);

            print(orientation.x);
            print(orientation.y);
            print(orientation.z);
        }

        if (Input.inputString == "d")
        {
            rotate(Vector3.forward);

            print(orientation.x);
            print(orientation.y);
            print(orientation.z);
        }

        //if (transform.position != center + orientation)
        //{
        //    c.orthographic = false;
        //}
        //else
        //{
        //    c.orthographic = true;
        //}
    }
}
