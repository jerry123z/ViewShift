using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

public class RelativeRotatorSystem : MonoBehaviour
{
    public static List<GameObject> selected;
    static int selection_index;

    void Start()
    {
        selected = new List<GameObject>();
        selection_index = 0;
    }

    void Update() {
        if (selected != null && selected.Count > 0){
            selected[0].GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    public static void RotateAll(GameObject center)
    {
        if (!center)
        {
            return;
        }
        var camera = GameObject.Find("Main Camera");
        Camera_Controller cameraController = camera.GetComponent<Camera_Controller>();
        GameObject relativeRotators = GameObject.Find("RelativeRotators");
        Transform transform = relativeRotators.GetComponent<Transform>();
        GameObject[] childsG = new GameObject[transform.childCount];
        int i = 0;
        foreach(Transform child in transform)
        {
            childsG[i] = child.gameObject;
            i++;
        }
        foreach(var child in childsG)
        {
            RelativeRotatorData relativeRotatorData = child.GetComponent<RelativeRotatorData>();
            if (relativeRotatorData.willRotate) {
                var childTransform = child.GetComponent<Transform>();
                childTransform.RotateAround(center.transform.position, Vector3.up, cameraController.speed * cameraController.direction);
            }
        }
    }

    public static void SelectAllInDirection(Vector3 position, Vector3 direction)
    {
        if (selected != null && selected.Count > 0)
        {
            List<GameObject> inDirection = new List<GameObject>();
            //foreach (GameObject possible in selected)
            //{
            //    if (Vector3.Dot((possible.transform.position - position), direction) >= 0)
            //    {
            //        inDirection.Add(possible);
            //    }
            //}
            RaycastHit hit;
            int mask = LayerMask.GetMask("PushableBlock");
            if (Physics.SphereCast(position, 0.5f, direction, out hit, direction.magnitude, mask))
            {
                inDirection.Add(hit.transform.gameObject);
            }

            if (inDirection.Count > 0)
            {
                ReleaseAll();
                selected = inDirection;
                foreach (GameObject child in selected)
                {
                    // need a different glow for selecting
                    child.GetComponent<Animator>().SetBool("Glow", true);
                }
                // print("selected.count: " + selected.Count);
                print("selection_index is: " + selection_index);
                RelativeRotatorData rrd = selected[selection_index].GetComponent<RelativeRotatorData>();
                rrd.willRotate = true;
                if (selected[selection_index].GetComponent<Rigidbody>()) {
                    selected[selection_index].GetComponent<Rigidbody>().useGravity = false;
                    // selected[selection_index].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    selected[selection_index].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                selected[selection_index].GetComponent<Animator>().SetBool("Selected", true);
            }
        }
    }


    public static void SelectAllInView(Vector3 position, double ViewRadius)
    {
        GameObject relativeRotators = GameObject.Find("RelativeRotators");
        Transform transforms = relativeRotators.GetComponent<Transform>();

        selected = new List<GameObject>();

        foreach (Transform child in transforms)
        {
            if ((child.position - position).magnitude <= ViewRadius)
            {
                // should also check that there's no wall between player and object candidate (raycast from position)
                selected.Add(child.gameObject);

                // need a different glow for selecting
                child.gameObject.GetComponent<Animator>().SetBool("Glow", true);
            }
        }
        print("selected.count: " + selected.Count);
        //if (selected.Count > 0)
        //{
        //    RelativeRotatorData rrd = selected[0].GetComponent<RelativeRotatorData>();
        //    rrd.willRotate = true;
        //    selected[selection_index].GetComponent<Animator>().SetBool("Selected", true);
        //}
    }

    public static void Scroll()
    {
        print("currently selecting: " + selection_index);
        print("selected.count: " + selected.Count);
        if (selected.Count > 0)
        {
            GameObject child;
            child = selected[selection_index];

            RelativeRotatorData rrd;
            rrd = child.gameObject.GetComponent<RelativeRotatorData>();
            rrd.willRotate = false;
            child.GetComponent<Animator>().SetBool("Selected", false);
            selection_index = (selection_index + 1) % selected.Count;
            child = selected[selection_index];
            rrd = child.GetComponent<RelativeRotatorData>();
            rrd.willRotate = true;
            child.GetComponent<Rigidbody>().useGravity = rrd.usesGravity;
            child.gameObject.GetComponent<Animator>().SetBool("Selected", true);
        }
    }

    public static void ReleaseAll()
    {
        GameObject relativeRotators = GameObject.Find("RelativeRotators");
        Transform transforms = relativeRotators.GetComponent<Transform>();
        foreach (Transform child in transforms)
        {
            var rrd = child.gameObject.GetComponent<RelativeRotatorData>();
            rrd.willRotate = false;
            if (child.gameObject.GetComponent<Rigidbody>()) {
                child.gameObject.GetComponent<Rigidbody>().useGravity = rrd.usesGravity;
            }
            child.gameObject.GetComponent<Animator>().SetBool("Glow", false);
            child.gameObject.GetComponent<Animator>().SetBool("Selected", false);
        }
        selected = null;
    }

    public static void Unfreeze()
    {
        // GameObject relativeRotators = GameObject.Find("RelativeRotators");
        // Transform transforms = relativeRotators.GetComponent<Transform>();
        // foreach (Transform child in transforms)
        // {
        //     if (child.gameObject.GetComponent<Rigidbody>()) {
        //         // child.gameObject.GetComponent<Rigidbody>().constraints= RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
        //         // child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //         // var rrd = child.gameObject.GetComponent<RelativeRotatorData>();
        //         // child.gameObject.GetComponent<Rigidbody>().useGravity = true;
        //         // child.gameObject.GetComponent<Rigidbody>().useGravity = true;
        //     }
        // }
    }

    public static void Freeze()
    {
        // GameObject relativeRotators = GameObject.Find("RelativeRotators");
        // Transform transforms = relativeRotators.GetComponent<Transform>();
        // foreach (Transform child in transforms)
        // {
        //     if (child.gameObject.GetComponent<Rigidbody>())
        //     {
        //         // child.gameObject.GetComponent<Rigidbody>().useGravity = false;
        //         // child.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //         // child.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //         child.gameObject.GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezeAll;
        //     }
        // }
    }
}
