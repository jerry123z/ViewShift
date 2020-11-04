using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform center;
    public Vector3 orientation;
    public Vector3 top_face;
    private Vector3 height;
    private Quaternion isometricOffset;   
    public float scale;
    public float speed;
    public float direction;
    public Camera c;
    public bool isRotating;
    private float rotateTimer;
    public GameObject cubes;
    public GameObject player;

    public Vector3 up;
    void Start()
    {
        top_face = new Vector3(0, 1, 0);
        scale = 10f;
        speed = 3f;
        direction = -1f;
        orientation =  new Vector3(1, 0, 0);
        height = new Vector3 (0,5,0);
        center = player.GetComponent<Transform>();
        isometricOffset = Quaternion.Euler(0, -45, 0);
        transform.position = height + center.position +  scale * (isometricOffset * orientation);
        transform.LookAt(center.position);
        isRotating = false;
        rotateTimer = 0;
        c = GetComponent<Camera>();
        up = Vector3.up;
    }

    void rotate(Vector3 direction)
    {
        if (direction == Vector3.left){
            orientation = Quaternion.Euler(0, 90, 0) * orientation;
        } else if (direction == Vector3.right){
            orientation = Quaternion.Euler(0, -90, 0) * orientation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating && rotateTimer > 0){
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            float step = speed * Time.deltaTime; // calculate distance to move
            GameObject relativeRotators = GameObject.Find("RelativeRotators");
            Transform transforms = relativeRotators.GetComponent<Transform>();
            foreach (Transform child in transforms)
            {
                child.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            RelativeRotatorSystem.RotateAll();
            transform.RotateAround(center.position, Vector3.up, speed*direction);
            //transform.position = Vector3.MoveTowards(transform.position, height + center.position +  scale * (isometricOffset * orientation), step);
        } else{
            GameObject relativeRotators = GameObject.Find("RelativeRotators");
            Transform transforms = relativeRotators.GetComponent<Transform>();
            foreach (Transform child in transforms)
            {
                child.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            //print("isRotating = false");
            isRotating = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            float step = speed * 20 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, height + center.position +  scale * (isometricOffset * orientation), step);
        }

        if (Input.GetButtonDown("Fire3")){
            var transform = player.GetComponent<Transform>();
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                if (hit.transform.gameObject.CompareTag("RelativeRotator"))
                {
                    var rrd = hit.transform.gameObject.GetComponent<RelativeRotatorData>();
                    rrd.willRotate = !(rrd.willRotate);
                }
                else {
                    center = hit.transform;
                }
            }
        }
        if (Input.GetButtonDown("Submit")){
            print("Submit");
            center = player.GetComponent<Transform>();;
            print(center.position);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (transform.position == height + center.position + scale * (isometricOffset * orientation) && isRotating == false)
            {
                //scale_down_faces();
                //player.GetComponent<PlayerMover>().snap();
                isRotating = true;
                transform.position = height + center.position + scale * (isometricOffset * orientation);
                rotate(Vector3.left);
                direction = 1f;
                rotateTimer = 90;
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (transform.position == height + center.position + scale * (isometricOffset * orientation) && isRotating == false)
            {
                //scale_down_faces();
                //player.GetComponent<PlayerMover>().snap();
                isRotating = true;
                transform.position = height + center.position + scale * (isometricOffset * orientation);
                rotate(Vector3.right);
                direction = -1f;
                rotateTimer = 90;
            }
        }

        //Vector3 target = height + center.position + scale * (isometricOffset * orientation);

        //if (Mathf.Sqrt(Mathf.Pow((transform.position - target).x, 2) + Mathf.Pow((transform.position - target).z, 2)) < 0.1f && isRotating == true)
        //{
        //   scale_up_faces();
        //   isRotating = false;
        //   //player.GetComponent<PlayerMover>().snap();
        //}
    }

    void scale_up_faces(){
        GameObject cubes = GameObject.Find("Cubes");
        BoxCollider[] boxColliders = cubes.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider boxCollider in boxColliders){
            var s = boxCollider.size;
            Vector3 factor = orientation;
            factor *= 15;
            factor += new Vector3(1, 1, 1);
            s = new Vector3 ((factor.x)*s[0], (factor.y)*s[1], factor.z * s[2]);
            boxCollider.size = s;
        }
    }
    
    void scale_down_faces(){
        //GameObject cubes = GameObject.Find("Cubes");
        BoxCollider[] boxColliders = cubes.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider boxCollider in boxColliders){
            var s = boxCollider.size;
            Vector3 factor = orientation;
            factor *= 15;
            factor += new Vector3(1, 1, 1);
            s = new Vector3((factor.x)/s.x, (factor.y)/s.y, (factor.z)/s.z);
            boxCollider.size = s;
        }
    }
}
