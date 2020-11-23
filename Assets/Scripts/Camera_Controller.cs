﻿using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform center;
    public Vector3 orientation;
    public Vector3 top_face;
    public Vector3 height;
    private Quaternion isometricOffset;   
    public float scale;
    public float speed;
    public float direction;
    public Camera c;
    public bool isRotating;
    private float rotateTimer;
    public GameObject player;
    private PlayerMover playerMover;
    public AudioClip rotateClip1;
    public AudioClip rotateClip2;
    private AudioSource audioSource;

    public Vector3 up;
    void Start()
    {
        top_face = new Vector3(0, 1, 0);
        scale = 10f;
        speed = 3f;
        direction = -1f;
        orientation =  new Vector3(1, 0, 0);
        height = new Vector3 (0,6,0);
        center = player.GetComponent<Transform>();
        isometricOffset = Quaternion.Euler(0, -45, 0);
        transform.position = height + center.position +  scale * (isometricOffset * orientation);
        transform.LookAt(center.position);
        isRotating = false;
        rotateTimer = 0;
        playerMover = player.GetComponent<PlayerMover>();
        c = GetComponent<Camera>();
        up = Vector3.up;
        audioSource = GetComponent<AudioSource>();
        c.depthTextureMode = DepthTextureMode.DepthNormals;
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
        if (isRotating && rotateTimer > 0) {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            float step = speed * Time.deltaTime; // calculate distance to move
            RelativeRotatorSystem.RotateAll(playerMover.touching);
            transform.RotateAround(center.position, Vector3.up, speed * direction);
            rotateTimer -= speed;
            //transform.position = Vector3.MoveTowards(transform.position, height + center.position +  scale * (isometricOffset * orientation), step);
        } else if(isRotating) {
            isRotating = false;
            RelativeRotatorSystem.Unfreeze();
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        } else {            
            float step = speed * 20 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, height + center.position + scale * (isometricOffset * orientation), step);
        }

        //if (Input.GetButtonDown("Fire Out")){
        //    var transform = player.GetComponent<Transform>();
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit)){
        //        DrawLine(transform.position, hit.point, Color.red, 0.1f);
        //        if (hit.transform.gameObject.CompareTag("RelativeRotator"))
        //        {
        //            var rrd = hit.transform.gameObject.GetComponent<RelativeRotatorData>();
        //            rrd.willRotate = !(rrd.willRotate);
        //            var glow = !hit.transform.gameObject.GetComponent<Animator>().GetBool("Glow");
        //            hit.transform.gameObject.GetComponent<Animator>().SetBool("Glow", glow);
        //        } else {
        //            //center.GetComponent<Animator>().SetBool("Glow", false);
        //            center = hit.transform.parent;
        //            //center.GetComponent<Animator>().SetBool("Glow", true);
        //        }
        //    }
        //}
        //if (Input.GetButtonDown("Fire Self")){
        //    //center.GetComponent<Animator>().SetBool("Glow", false);
        //    center = player.GetComponent<Transform>();
        //    print(center.position);
        //}

        if (Input.GetButtonDown("Fire Self"))
        {
            // need to tweak ViewRadius parameter later to fit into stuff thats within view
            RelativeRotatorSystem.SelectAllInView(player.transform.position, scale * 1.4);
        }

        if (Input.GetButtonDown("Fire Out"))
        {
            RelativeRotatorSystem.Scroll();
        }


        if (Input.GetButtonDown("Reset All"))
        {
            RelativeRotatorSystem.ReleaseAll();
        }

        if (Input.GetButtonDown("Rotate Right"))
        {
            if (transform.position == height + center.position + scale * (isometricOffset * orientation) && isRotating == false)
            {
                audioSource.PlayOneShot(rotateClip1, 0.5f);
                isRotating = true;
                transform.position = height + center.position + scale * (isometricOffset * orientation);
                rotate(Vector3.left);
                direction = 1f;
                rotateTimer = 90;
                RelativeRotatorSystem.Freeze();
            }
        }

        if (Input.GetButtonDown("Rotate Left"))
        {
            if (transform.position == height + center.position + scale * (isometricOffset * orientation) && isRotating == false)
            {
                audioSource.PlayOneShot(rotateClip2, 0.5f);
                isRotating = true;
                transform.position = height + center.position + scale * (isometricOffset * orientation);
                rotate(Vector3.right);
                direction = -1f;
                rotateTimer = 90;
                RelativeRotatorSystem.Freeze();
            }
        }
    }

    void disableAllAnimator()
    {
        GameObject relativeRotators = GameObject.Find("RelativeRotators");
        Transform transform = relativeRotators.GetComponent<Transform>();
        foreach (Transform rotator in transform)
        {
            GameObject obj = rotator.gameObject;
            if (obj.GetComponent<Animator>().enabled)
            {
                obj.GetComponent<Animator>().enabled = false;
            }
        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Specular"));
        print(Shader.Find("Specular").name);
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}
