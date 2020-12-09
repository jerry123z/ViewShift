using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    public float Speed = 1f;
    public float JumpHeight = 1f;
    public float GroundDistance = 0.2f;
    private float distToGround;

    public Camera cam;
    private BoxCollider bc;
    private Rigidbody _body;
    public Vector3 _inputs = Vector3.zero;
    public bool _isGrounded = true;

    public GameObject touching;
    public GameObject starting;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip fall;
    public AudioClip reset;
    private AudioSource audioSource;
    private bool _respawning;

    void Start()
    {
        transform.position = starting.transform.position + Vector3.up * 2;
        //print("starting at " + transform.positiosn);
        _body = GetComponent<Rigidbody>();
        bc = transform.GetComponent<BoxCollider>();
        distToGround = bc.bounds.extents.y;
        audioSource = GetComponent<AudioSource>();
        _respawning = false;
    }

    void Update()
    {
        if (checkBottom() != _isGrounded)
        {
            _isGrounded = !_isGrounded;
            if (_body.velocity.y < -3) { 
                audioSource.PlayOneShot(land, 0.7f);
            }
        }
        Quaternion offset = Quaternion.Euler(0, -45, 0);
        _inputs = Vector3.zero;
        _inputs += Input.GetAxis("Horizontal") * Vector3.Cross(-1 * Vector3.up, offset * cam.GetComponent<Camera_Controller>().orientation);
        _inputs += Input.GetAxis("Vertical") * -1 * (offset* cam.GetComponent<Camera_Controller>().orientation);

        //print(_inputs);
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            audioSource.PlayOneShot(jump, 0.5f);
            _body.AddForce(5f * Vector3.up * Mathf.Sqrt(JumpHeight), ForceMode.VelocityChange);
        }

        if ((transform.position.y - starting.transform.position.y) < -2 && !_respawning)
        {
            audioSource.PlayOneShot(fall, 0.7f);
            _respawning = true;
        }

        if ((transform.position.y - starting.transform.position.y) < -12)
        {
            audioSource.PlayOneShot(reset, 0.7f);
            transform.position = starting.transform.position + Vector3.up * 2;
            //cam.GetComponent<Camera_Controller>().up = Vector3.up;
            Physics.gravity = -1 * Vector3.up * 9.8f;
            _body.velocity = Vector3.zero;
            _respawning = false;
        }
    }

    bool checkBottom()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;

        //print(Physics.Raycast(ray, out hit, (float)(distToGround + 0.1)));
        if (Physics.Raycast(ray, out hit, (float)(distToGround)))
        {
            //if (hit.transform.parent != null)
            if (hit.transform.gameObject.CompareTag("RotatorZone") && hit.transform.gameObject != touching)
            {
                RemoveOutline();
                Renderer[] renderers = hit.transform.GetComponentsInChildren<Renderer>();
                //Outline outline = hit.transform.parent.gameObject.AddComponent<Outline>();
                foreach (Renderer child in renderers)
                {
                    Material[] materials = child.materials;
                    foreach (Material mat in materials)
                    {
                        mat.SetColor("_EmissionColor", new Color(0.6f, 0.6f, 0.6f, 0.6f));
                    }
                }
                //outline.OutlineMode = Outline.Mode.OutlineAll;
                touching = hit.transform.gameObject;
            }
            else if (hit.transform.gameObject.CompareTag("RotatorZone"))
            {
                //pass
            }
            else
            {
                RemoveOutline();
            }
        } else
        {
            RemoveOutline();
        }
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround)) ||
        Physics.Raycast(transform.position + new Vector3(0, 0, 0.5f), -Vector3.up, (float)(distToGround)) ||
        Physics.Raycast(transform.position + new Vector3(0, 0, -0.5f), -Vector3.up, (float)(distToGround)) ||
        Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), -Vector3.up, (float)(distToGround)) ||
        Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), -Vector3.up, (float)(distToGround));
    }

    void RemoveOutline()
    {
        if (touching)
        {
            Renderer[] renderers = touching.GetComponentsInChildren<Renderer>();
            //Outline outline = hit.transform.parent.gameObject.AddComponent<Outline>();
            foreach (Renderer child in renderers)
            {
                Material[] materials = child.materials;
                foreach (Material mat in materials)
                {
                    mat.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
                }
            }
            touching = null;
        }
    }

    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}