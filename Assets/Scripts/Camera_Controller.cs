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

    public GameObject touching;
    void Start()
    {
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
        transform.LookAt(center);


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
            
        }

        if (transform.position == center + scale * orientation && isRotating == true)
        {
           scale_up_faces();
           isRotating = false;
        }
    }

    void scale_up_faces(){
        print("scale up");
        GameObject cubes = GameObject.Find("Cubes");
        BoxCollider[] boxColliders = cubes.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider boxCollider in boxColliders){
            var s = boxCollider.size;
            Vector3 factor = orientation;
            factor *= 63;
            factor += new Vector3(1, 1, 1);
            s = new Vector3 ((factor.x)*s[0], (factor.y)*s[1], factor.z * s[2]);
            boxCollider.size = s;
        }
    }
    
    void scale_down_faces(){
        print("scale down");
        //GameObject cubes = GameObject.Find("Cubes");
        BoxCollider[] boxColliders = cubes.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider boxCollider in boxColliders){
            var s = boxCollider.size;
            Vector3 factor = orientation;
            factor *= 63;
            factor += new Vector3(1, 1, 1);
            s = new Vector3((factor.x)/s.x, (factor.y)/s.y, (factor.z)/s.z);
            boxCollider.size = s;
        }
    }
}
