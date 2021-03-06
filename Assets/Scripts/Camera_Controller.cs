﻿using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform center;
    public Vector3Int orientation;
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
    public AudioClip slidingClip;
    private AudioSource audioSource;

    void Start()
    {
        top_face = new Vector3(0, 1, 0);
        scale = 30f;
        speed = 3f;
        direction = -1f;
        orientation =  new Vector3Int(1, 0, 0);
        height = new Vector3 (0,18,0);
        center = player.GetComponent<Transform>();
        isometricOffset = Quaternion.Euler(0, -45, 0);
        transform.position = height + center.position +  scale * (isometricOffset * orientation);
        transform.LookAt(center.position);
        isRotating = false;
        rotateTimer = 0;
        playerMover = player.GetComponent<PlayerMover>();
        c = GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
        c.depthTextureMode = DepthTextureMode.DepthNormals;
    }

    void rotate(Vector3 direction)
    {
        if (direction == Vector3.left){
            orientation = Vector3Int.RoundToInt(Quaternion.Euler(0, 90, 0) * orientation);
        } else if (direction == Vector3.right){
            orientation = Vector3Int.RoundToInt(Quaternion.Euler(0, -90, 0) * orientation);
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

        Vector3 C = Vector3.zero;
        Quaternion offset = Quaternion.Euler(0, -45, 0);
        C = Vector3.zero;
        C += Input.GetAxis("FireHorizontal") * Vector3.Cross(-1 * Vector3.up, offset * orientation);
        C += Input.GetAxis("FireVertical") * (offset * orientation);
        if (C.magnitude >= 0.2)
        {
            C = (scale * 0.6f) * C.normalized;
            var player_transform = player.GetComponent<Transform>();
            Vector3 B = player_transform.position;
            RelativeRotatorSystem.SelectAllInDirection(B, C);
            //DrawLine(B, B + C, Color.red, 0.1f);
        }

        if (Input.GetButtonDown("Select In View"))
        {
            // need to tweak ViewRadius parameter later to fit into stuff thats within view
            if (RelativeRotatorSystem.selected != null && RelativeRotatorSystem.selected.Count > 0)
            {
                print("releasing all");
                RelativeRotatorSystem.ReleaseAll();
            }
            else
            {
                print("selecting in view");
                RelativeRotatorSystem.SelectAllInView(player.transform.position, scale * 2);
            }
        }
        
        if (Input.GetButtonDown("Rotate Right"))
        {
            if (transform.position == height + center.position + scale * (isometricOffset * orientation) && isRotating == false)
            {
                if (RelativeRotatorSystem.selected != null && RelativeRotatorSystem.selected.Count > 0)
                {
                    print("slide");
                    audioSource.PlayOneShot(slidingClip, 0.5f);
                }
                else
                {
                    audioSource.PlayOneShot(rotateClip1, 0.5f);
                }
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
                if (RelativeRotatorSystem.selected != null && RelativeRotatorSystem.selected.Count > 0)
                {
                    print("slide");
                    audioSource.PlayOneShot(slidingClip, 0.5f);
                }
                else
                {
                    audioSource.PlayOneShot(rotateClip2, 0.5f);
                }
                isRotating = true;
                transform.position = height + center.position + scale * (isometricOffset * orientation);
                rotate(Vector3.right);
                direction = -1f;
                rotateTimer = 90;
                RelativeRotatorSystem.Freeze();
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
        //print(Shader.Find("Specular").name);
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}
