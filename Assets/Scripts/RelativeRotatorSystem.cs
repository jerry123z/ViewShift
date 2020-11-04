using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class RelativeRotatorSystem : MonoBehaviour
{
    public static void RotateAll()
    {
        var camera = GameObject.Find("Main Camera");
        Camera_Controller cameraController = camera.GetComponent<Camera_Controller>();
        GameObject relativeRotators = GameObject.Find("RelativeRotators");
        Transform transform = relativeRotators.GetComponent<Transform>();
        GameObject[] childsG = new GameObject[transform.childCount];
        int i  = 0;
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
                childTransform.RotateAround(cameraController.center.position, Vector3.up, cameraController.speed * cameraController.direction);
            }
        }
    }

    public static void Unfreeze()
    {
        GameObject relativeRotators = GameObject.Find("RelativeRotators");
        Transform transforms = relativeRotators.GetComponent<Transform>();
        foreach (Transform child in transforms)
        {
            child.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public static void Freeze()
    {
        GameObject relativeRotators = GameObject.Find("RelativeRotators");
        Transform transforms = relativeRotators.GetComponent<Transform>();
        foreach (Transform child in transforms)
        {
            child.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
