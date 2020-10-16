using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    // Start is called before the first frame update
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
    public GameObject cubes;
    public GameObject player;

    public GameObject touching;

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
        scale_up_faces();
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
        //orientation = direction;
        //transform.position = center + orientation;
        //transform.LookAt(center);
    }

    // Update is called once per frame
    void Update()
    {
        print(transform.position);
        print(height + center.position +  scale * (isometricOffset * orientation));
        if (isRotating){
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.RotateAround(center.position, Vector3.up, speed*direction);
            //transform.position = Vector3.MoveTowards(transform.position, height + center.position +  scale * (isometricOffset * orientation), step);
        } else{
            float step = speed * 20 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, height + center.position +  scale * (isometricOffset * orientation), step);
        }

        if (Input.GetMouseButtonDown(0)){
            Ray ray = c.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                print("hit");
                center = hit.transform;
                print(center.position);
            }
        }

        if (Input.inputString == "a")
        {
            if (transform.position == height + center.position + scale * (isometricOffset * orientation) && isRotating == false)
            {
                scale_down_faces();
                isRotating = true;
                rotate(Vector3.left);
                player.GetComponent<PlayerMover>().snap();
                direction = 1f;
            }
        }

        if (Input.inputString == "d")
        {
            if (transform.position == height + center.position + scale * (isometricOffset * orientation) && isRotating == false)
            {
                scale_down_faces();
                isRotating = true;
                rotate(Vector3.right);
                player.GetComponent<PlayerMover>().snap();
                direction = -1f;
            }
        }

        if (transform.position == height + center.position + scale * (isometricOffset * orientation) && isRotating == true)
        {
           scale_up_faces();
           isRotating = false;
           player.GetComponent<PlayerMover>().snap();
        }
    }

    void scale_up_faces(){
        print("scale up");
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
        print("scale down");
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
