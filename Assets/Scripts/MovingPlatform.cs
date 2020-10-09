using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] points;
    public int point_index = 0;
    private Vector3 curr_target;
    public float tolerance;
    public float speed;
    public float delay;

    private float delay_start;
    public bool automatic;

    public Camera cam;
    private bool _isRotating;

    // Start is called before the first frame update
    void Start()
    {
        if(points.Length > 0)
        {
            curr_target = points[0];
        }
        tolerance = speed * Time.deltaTime;

        _isRotating = cam.GetComponent<Camera_Controller>().isRotating;
    }

    // Update is called once per frame
    void Update()
    {
        _isRotating = cam.GetComponent<Camera_Controller>().isRotating;
        if (transform.position != curr_target && !_isRotating)
        {
            MovePlatform();
        }
        else if (!_isRotating)
        {
            ChangeTarget();
        }
    }

    void MovePlatform()
    {
        Vector3 heading = curr_target - transform.position;
        transform.position += (heading/heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            transform.position = curr_target;
            delay_start = Time.time;
        }
    }

    void ChangeTarget()
    {
        if (automatic)
        {
            if (Time.time - delay_start > delay)
            {
                ToNextPoint();
            }
        }
    }

    public void ToNextPoint()
    {
        point_index++;
        if(point_index >= points.Length)
        {
            point_index = 0;
        }
        curr_target = points[point_index];
    }

    private void OnTriggerEnter(Collider c)
    {
        c.transform.parent = transform;
    }

    private void OnTriggerExit(Collider c)
    {
        c.transform.parent = null;
    }
}
