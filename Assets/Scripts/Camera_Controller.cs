using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 center;
    public Vector3 orientation;
    public Vector3 top_face;
    public float scale;
    public float speed;
    public Camera c;
    public bool isRotating;
    public GameObject cubes;
    public GameObject player;

    public Vector3 up;
    public Vector3 current_up;

    public GameObject touching;

    //public GameObject test;
    void Start()
    {
        print("level is " + Application.loadedLevelName);
        center = new Vector3(1, 3, -5) ;
        top_face = new Vector3(0, 1, 0);
        scale = 10f;
        speed = 30f;
        orientation =  Vector3.forward;
        transform.position = center + scale * orientation;
        transform.LookAt(center);
        isRotating = false;
        scale_up_faces();
        c = GetComponent<Camera>();

        up = Vector3.up;
        current_up = up;

        //test.transform.LookAt(up);
    }

    void rotate(Vector3 direction)
    {
        if (direction == Vector3.left){
            orientation = Quaternion.Euler(0, -90, 0) * orientation;
        } else if (direction == Vector3.right){
            orientation = Quaternion.Euler(0, 90, 0) * orientation;
        }
        //orientation = direction;
        //transform.position = center + orientation;
        //transform.LookAt(center);
    }

    // Update is called once per frame
    void Update()
    {

        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, center + scale * orientation, step);
        transform.LookAt(center, current_up);



        //transform.rotation = Quaternion.LookRotation(center - transform.position, current_up);

        current_up = Vector3.MoveTowards(current_up, up, 1 * Time.deltaTime);




        if (Application.loadedLevelName != "level3")
        {
            if (Input.inputString == "a")
            {
                if (transform.position == center + scale * orientation && isRotating == false)
                {
                    scale_down_faces();
                    isRotating = true;
                }
                rotate(Vector3.left);
                player.GetComponent<PlayerMover>().snap();

            }

            if (Input.inputString == "d")
            {

                if (transform.position == center + scale * orientation && isRotating == false)
                {
                    scale_down_faces();
                    isRotating = true;
                }
                rotate(Vector3.right);
                player.GetComponent<PlayerMover>().snap();
            }

            if (transform.position == center + scale * orientation && isRotating == true)
            {
                scale_up_faces();
                isRotating = false;
            }
        }
        else
        {
            if (Input.inputString == "j")
            {
                //print("center is " + center);
                //print("up is " + up);

                GameObject parent = player.GetComponent<PlayerMover>().touching;
                //print("parent tag" + parent.tag);
                if (parent.tag != "column")
                {
                    //print("forward is " + parent.transform.forward);
                    //print("current position is" + player.transform.position);
                    //print("calculating using parent pos" + parent.transform.position);
                    //print("calculating using parent forward" + parent.transform.forward);
                    //print("now it is " + player.transform.position);
                    player.transform.position = parent.transform.position + parent.transform.forward;
                    player.transform.SetParent(parent.transform, true);
                    up = Quaternion.Euler(0, 0, -90) * up;
                }
                //print(up);
            }
            if (Input.inputString == "l")
            {
                GameObject parent = player.GetComponent<PlayerMover>().touching;
                if (parent.tag != "column")
                {
                    player.transform.position = parent.transform.position + parent.transform.forward;
                    player.transform.SetParent(parent.transform, true);
                    Physics.gravity = -parent.transform.forward * 9.8f;
                    up = Quaternion.Euler(0, 0, 90) * up;
                }
            }

            if (current_up != up)
            {
                //print(current_up);

                player.GetComponent<Rigidbody>().useGravity = false;
                GameObject parent = player.GetComponent<PlayerMover>().touching;
                if (parent)
                {
                    //player.transform.parent = parent.transform;
                    //player.transform.localPosition = current_up;
                    Physics.gravity = -parent.transform.forward * 9.8f;
                    parent.transform.LookAt(parent.transform.position + current_up);
                }
            }
            else
            {
                player.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        //GameObject test = player.GetComponent<PlayerMover>().touching;
        //test.transform.position += test.transform.forward * Time.deltaTime;
    }

    void scale_up_faces(){
        //print("scale up");
        GameObject cubes = GameObject.Find("Cubes");
        BoxCollider[] boxColliders = cubes.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider boxCollider in boxColliders){
            var s = boxCollider.size;
            Vector3 factor = orientation;
            factor *= 63;
            factor += new Vector3(1, 1, 1);
            if (boxCollider.material.name == "testmaterial (Instance)")
            {
                s = new Vector3((factor.x) * s[0], (factor.y) * s[1], 1);
            }
            else
            {
                s = new Vector3((factor.x) * s[0], (factor.y) * s[1], factor.z * s[2]);
            }
            boxCollider.size = s;
        }
    }
    
    void scale_down_faces(){
        //print("scale down");
        //GameObject cubes = GameObject.Find("Cubes");
        BoxCollider[] boxColliders = cubes.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider boxCollider in boxColliders){
            var s = boxCollider.size;
            Vector3 factor = orientation;
            factor *= 63;
            factor += new Vector3(1, 1, 1);
            print(boxCollider.material.name);
            if (boxCollider.material.name == "testmaterial (Instance)")
            {
                print("checked");
                s = new Vector3((factor.x) / s.x, (factor.y) / s.y, 1);
            }
            else
            {
                s = new Vector3((factor.x) / s.x, (factor.y) / s.y, (factor.z) / s.z);
            }
            boxCollider.size = s;
        }
    }
}
