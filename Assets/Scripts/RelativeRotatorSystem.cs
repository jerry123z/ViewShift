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
        Transform[] childsT  = new Transform[transform.childCount];
        GameObject[] childsG = new GameObject[transform.childCount];
        int i  = 0;
        foreach(Transform child in transform)
        {
            childsT[i] = child;
            childsG[i] = child.gameObject;
            i++;
        }
        foreach(var child in childsT)
        {
            child.RotateAround(cameraController.center.position, Vector3.up, cameraController.speed*cameraController.direction);
        }
    }
}
